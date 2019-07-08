using System;
using AutoMapper;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Repositories;
using BlogDemo.Infrastructure.Resources;
using BlogDemo.Infrastructure.Services;
using BlogDemoApi.Exceptions;
using BlogDemoApi.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PostResource = BlogDemoApi.Resources.PostResource;

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
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
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
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //这些官网都有的，可以去看，连接在笔记中

            //映射
            services.AddAutoMapper(ctf=>
            {
                ctf.AddProfile<MypingProfile>();
            }, AppDomain.CurrentDomain.GetAssemblies());
            //验证resource
            services.AddTransient<IValidator<PostResource>, PostResourceValidator>();

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

        }

        public void Configure(IApplicationBuilder app,ILoggerFactory loggerFactory)
        {
            //在页面上直接显示数据
            app.UseDeveloperExceptionPage();

            //因为是API不建议在页面直接显示出来，建议使用json
            //app.UseExceptionHandler();
            app.UserMyExceptionHander(loggerFactory);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
