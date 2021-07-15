using System;
using System.Linq;
using System.Linq.Expressions;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.DAL
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        protected BankContext BankContext { get; set; }

        protected RepositoryBase(BankContext bankContext)
        {
            BankContext = bankContext;
        }
        public IQueryable<T> FindAll()
        {
            return BankContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return BankContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            BankContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            BankContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            BankContext.Set<T>().Remove(entity);
        }
    }
}
