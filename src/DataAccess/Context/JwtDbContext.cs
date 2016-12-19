using LegnicaIT.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LegnicaIT.DataAccess.Context
{
    public class JwtDbContext : DbContext, IJwtDbContext
    {
        //public JwtDbContext(DbContextOptions options) : base(options)
        //{
        //}

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public DbSet<User> Users { set; get; }
        public DbSet<UserApps> UserApps { set; get; }
        public DbSet<App> Apps { set; get; }
    }
}
