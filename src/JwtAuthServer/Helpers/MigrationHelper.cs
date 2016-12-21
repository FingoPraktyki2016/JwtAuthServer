using LegnicaIT.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LegnicaIT.JwtAuthServer.Helpers
{
    public class MigrationHelper
    {
        // GET: /<controller>/
        public void Migrate(IConfigurationRoot ApplicationConfiguration)
        {
            var options = new DbContextOptionsBuilder<JwtDbContext>();
            options.UseSqlServer(ApplicationConfiguration.GetConnectionString("Database"), assembly => assembly.MigrationsAssembly("JwtAuthServer"));

            var context = new JwtDbContext(options.Options);

            var listofmigrations = context.Database.GetMigrations();
            Debug.WriteLine("Migrations " + listofmigrations.Count());

            var plistofmigrations = context.Database.GetPendingMigrations();
            Debug.WriteLine("Pending Migrations " + listofmigrations.Count());

            //context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}