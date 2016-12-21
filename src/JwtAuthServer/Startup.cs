﻿using LegnicaIT.JwtAuthServer.DependencyInjection;
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
using LegnicaIT.DataAccess.Repositories.Interfaces;

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

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            string debugValue = Configuration.GetSection("Logging:Loglevel:Default").Value;
            var logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), debugValue);

            loggerFactory.AddConsole(Configuration.GetSection("Logging")).AddDebug(logLevel);

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            var authHelper = new JwtAuthorizeHelper();
            authHelper.Configure(app);

            app.UseMvc();

            var databaseMigrationService = new MigrationHelper();
            databaseMigrationService.Migrate(Configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();

            services.AddEntityFramework().AddDbContext<JwtDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database"), assembly => assembly.MigrationsAssembly("JwtAuthServer")));

            var dependencyBuilder = new DependencyBuilder<IRepository>();
            dependencyBuilder.RegisterRepositories(services);
        }
    }
}