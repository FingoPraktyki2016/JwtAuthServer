using LegnicaIT.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LegnicaIT.JwtAuthServer.Helpers
{
    public class MigrationHelper
    {
        public void Migrate(IConfigurationRoot ApplicationConfiguration)
        {
            var options = new DbContextOptionsBuilder<JwtDbContext>();
            options.UseSqlServer(ApplicationConfiguration.GetConnectionString("Database"), assembly => assembly.MigrationsAssembly("JwtAuthServer"));

            var context = new JwtDbContext(options.Options);
            context.Database.Migrate();
        }
    }
}