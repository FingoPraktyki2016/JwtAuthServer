using LegnicaIT.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LegnicaIT.DataAccess.Context
{
    public interface IJwtDbContext
    {
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        void SetModified(object entity);
    }
}