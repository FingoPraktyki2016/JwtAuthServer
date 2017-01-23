using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LegnicaIT.DataAccess.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected DbSet<T> dbSet;
        protected IJwtDbContext context;

        protected GenericRepository(IJwtDbContext _context)
        {
            context = _context;
            dbSet = _context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            var timeNow = DateTime.UtcNow;
            entity.ModifiedOn = timeNow;
            entity.CreatedOn = timeNow;
            dbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            entity.DeletedOn = DateTime.UtcNow;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = context.Set<T>().Where(predicate);
            return query.DefaultIfEmpty().Equals(null) ? null : query;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public virtual T GetById(int id)
        {
            return dbSet.FirstOrDefault(m => m.Id == id);
        }

        public virtual void Edit(T entity)
        {
            context.SetModified(entity);
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }
    }
}