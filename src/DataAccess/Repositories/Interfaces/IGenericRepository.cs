﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LegnicaIT.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);

        void Delete(T entity);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();

        T GetById(int id);

        void Save();
    }
}
