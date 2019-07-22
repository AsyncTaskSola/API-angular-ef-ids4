using System;
using System.Linq;
using AutoMapper;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Repositories;
using BlogDemo.Infrastructure.Resources;
using BlogDemo.Infrastructure.Services;
using BlogDemoApi.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace BlogDemoApi
{
    public class StartupDevelopment
    {
        private readonly IConfiguration _configuration;

        public StartupDevelopment(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {//406输入格式支持问题，默认application/json 现在添加application/xml的支持
                options.ReturnHttpNotAcceptable = true;
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

                //创建特定的媒体类型(输出)
                var outputFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                if (outputFormatter != null)
                {
                    outputFormatter.SupportedMediaTypes.Add("application/vnd.cgzl.hateoas+json");
                }
            })
            //添加字段限制
            .AddFluentValidation()
                //这只是小写
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new
                                  CamelCasePropertyNamesContractResolver();
                });
            //ef 直接建立到本地的了
            services.AddDbContext<MyContext>(
                options =>
                {
                    //var connectionstrings = _configuration["ConnectionStrings:DefaultConnection"];
                    // var connectionstring = _configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlite("Data Source=BlogDemo.db");
                });

            //https
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            #region identityServer4

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.ApiName = "restapi";
                });


            #endregion

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //这些官网都有的，可以去看，连接在笔记中

            //映射
            services.AddAutoMapper(ctf =>
            {
                ctf.AddProfile<MypingProfile>();
            }, AppDomain.CurrentDomain.GetAssemblies());

            //验证resource 包含约束
            //  services.AddTransient<IValidator<PostAddResource>, PostAddResourceValidator>();

            services.AddTransient<IValidator<PostAddResource>, PostAddOrUpdateResourceValidator<PostAddResource>>();
            services.AddTransient<IValidator<PostUpdateResource>, PostAddOrUpdateResourceValidator<PostUpdateResource>>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            //注册容器
            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<PostPropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);

            //验证字段 临时服务
            services.AddTransient<ITypeHelperService, TypeHelperService>();

            //跨域

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevOrign"
                    , builder =>
                        builder.WithOrigins("http://localhost:4200")//起源
                        .WithHeaders("X-pagination")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });


            //https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2
            //全局加入身份验证  
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAngularDevOrign"));
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            //在页面上直接显示数据
            app.UseDeveloperExceptionPage();

            //因为是API不建议在页面直接显示出来，建议使用json
            //app.UseExceptionHandler();
            app.UserMyExceptionHander(loggerFactory);
            app.UseCors("AllowAngularDevOrign");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
