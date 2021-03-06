﻿using LegnicaIT.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LegnicaIT.DataAccess.Context
{
    public class JwtDbContext : DbContext, IJwtDbContext
    {
        public JwtDbContext()
        {
        }

        public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=JwtAuthServer;Integrated Security=True;");
            }
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void PerformMigration()
        {
            Database.Migrate();
        }

        #region DbSet

        public DbSet<User> Users { get; set; }

        public DbSet<UserApps> UserApps { get; set; }

        public DbSet<App> Apps { get; set; }

        #endregion DbSet
    }
}
