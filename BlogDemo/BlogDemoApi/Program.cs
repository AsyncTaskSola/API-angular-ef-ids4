using System;
using System.Reflection;
using BlogDemo.Infrastructure.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlogDemoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope=host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerfactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var mycontext = services.GetRequiredService<MyContext>();
                    MyContextSeed.SeedAsync(mycontext, loggerfactory).Wait();
                }
                catch (Exception e)
                {
                    var logger = loggerfactory.CreateLogger<Program>();
                    logger.LogError(e,"Error ouccured seeding the Datebase");//数据库传输错误
                }

            }
              host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               // .UseStartup<Startup>();
               .UseStartup(typeof(StartupDevelopment).GetTypeInfo().Assembly.FullName);
    }
}
