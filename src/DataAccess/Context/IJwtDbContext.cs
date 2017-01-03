using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LegnicaIT.DataAccess.Context
{
    public interface IJwtDbContext : IRepository
    {
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        void SetModified(object entity);

        void PerformMigration();
    }
}