using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Configuration.Helpers;
using LegnicaIT.DataAccess.Context;
using LegnicaIT.JwtManager.Services.Implementation;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LegnicaIT.JwtManager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework().AddDbContext<JwtDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            RegisterDependecy.Register(services);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(ISessionService<>), typeof(SessionService<>));

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddOptions();
            services.Configure<ManagerSettings>(Configuration.GetSection("ManagerSettings"));
            services.Configure<LoggerConfig>(Configuration.GetSection("Logging:Loglevel"));
            services.Configure<EmailServiceCredentials>(Configuration.GetSection("EmailServiceCredentials"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var debugValue = Configuration.GetSection("Logging:Loglevel:Default").Value;
            var logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), debugValue);

            //I'm gonna leave it as string array becase we might want to add some log modules later
            string[] logOnlyThese = { }; // or reverse string[] dontlong = {"ObjectResultExecutor", "JsonResultExecutor"};

            loggerFactory.AddDebug((category, _logLevel) => (!logOnlyThese.Any(category.Contains) && _logLevel >= logLevel));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id}");
            });
        }
    }
}