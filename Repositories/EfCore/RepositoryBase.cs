using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryDbContext _repositoryDbContext;

        public RepositoryBase(RepositoryDbContext repositoryDbContext)
        {
            _repositoryDbContext = repositoryDbContext;
        }

        public void Create(T entity) => _repositoryDbContext.Set<T>().Add(entity);
        

        public void Delete(T entity) => _repositoryDbContext.Set<T>().Remove(entity);


        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? _repositoryDbContext.Set<T>().AsNoTracking() :
            _repositoryDbContext.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trachChanges) =>
            !trachChanges ? _repositoryDbContext.Set<T>().Where(expression).AsNoTracking() :
            _repositoryDbContext.Set<T>().Where(expression);
      

        public void Update(T entity) => _repositoryDbContext.Set<T>().Update(entity);
        
    }
}
