using LegnicaIT.BusinessLogic.Configuration.Helpers;
using LegnicaIT.BusinessLogic.Configuration.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.DataAccess.Context;
using LegnicaIT.JwtAuthServer.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace LegnicaIT.JwtAuthServer
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IMigrationHelper migrationHelper)
        {
            string debugValue = Configuration.GetSection("Logging:Loglevel:Default").Value;
            var logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), debugValue);

            //I'm gonna leave it as string array becase we might want to add some log modules later
            string[] logOnlyThese = { "WebHost" }; // or reverse string[] dontLog = {"ObjectResultExecutor", "JsonResultExecutor"};

            loggerFactory.AddDebug((category, _logLevel) => (logOnlyThese.Any(category.Contains) && _logLevel >= logLevel));

            var authHelper = new JwtAuthorizeHelper();
            authHelper.Configure(app);

            app.UseMvc();

            migrationHelper.Migrate();

            app.UseCors("AnyOrigin");
        }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            var corsBuilder = new CorsPolicyBuilder();

            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", corsBuilder.Build());
            });

            services.AddEntityFramework().AddDbContext<JwtDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.Configure<LoggerConfig>(Configuration.GetSection("Logging:Loglevel"));

            RegisterDependecy.Register(services);
        }
    }
}