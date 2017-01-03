using LegnicaIT.BusinessLogic.Configuration.Interfaces;
using LegnicaIT.DataAccess.Context;

namespace LegnicaIT.BusinessLogic.Configuration.Implementation
{
    public class MigrationHelper : IMigrationHelper
    {
        private readonly IJwtDbContext jwtDbContext;
        public MigrationHelper(IJwtDbContext jwtDbContext)
        {
            this.jwtDbContext = jwtDbContext;
        }

        public void Migrate()
        {
            jwtDbContext.PerformMigration();
        }
    }
}