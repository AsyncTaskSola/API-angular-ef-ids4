using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BlogDemoApi.Exceptions
{
    /// <summary>
    /// 异常处理扩展
    /// </summary>
    public static class ExceptionHandingExtensions
    {
        public static void UserMyExceptionHander(this IApplicationBuilder app,ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var loger = loggerFactory.CreateLogger("BlogDemoApi.Exceptions.ExceptionHandingExtensions");
                        loger.LogError(500,ex.Error,ex.Error.Message);
                    }

                    await context.Response.WriteAsync(ex?.Error?.Message ?? "An Error Occurred");
                });
            });
        }
    }
}
