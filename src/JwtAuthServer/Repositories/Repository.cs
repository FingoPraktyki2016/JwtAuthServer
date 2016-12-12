using LegnicaIT.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JwtAuthServer
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IJwtDbContext _db;

        public UserRepository(IJwtDbContext context) : base(context)
        {
            _db = context;
        }
    }

    public interface IUserRepository
    {
    }

    public interface IJwtDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        EntityEntry Entry(object entity);

        int SaveChanges();
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected IJwtDbContext Entities;
        protected readonly DbSet<T> Dbset;

        protected GenericRepository(IJwtDbContext context)
        {
            Entities = context;
            Dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        T GetById(int id);
    }
}