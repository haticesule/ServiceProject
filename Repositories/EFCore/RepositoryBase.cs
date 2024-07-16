using Entitiess.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            var newObject =  _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return newObject.Entity;
        }
 
        public void Delete(T entity) =>_context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ?
        _context.Set<T>().AsNoTracking() :
        _context.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, 
            bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().Where(expression).AsNoTracking() :
            _context.Set<T>().Where(expression);
        

        public void Update(T entity) => _context.Set<T>().Update(entity);
         
        
    }
}
