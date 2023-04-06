using DemoEmployeeApi.Repo.Data;
using DemoEmployeeApi.Repo.GenericRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Repo.GenericRepository.Service
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private RepositoryContext _repositoryContext;
        private DbSet<T> entities;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            entities=_repositoryContext.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _repositoryContext.SaveChanges();
        }

        public void Update(T? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity Null");
            }
            entities.Update(entity);
            _repositoryContext.SaveChanges();
        }

        public void Remove(T? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _repositoryContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _repositoryContext.SaveChanges();
        }       
    }
}
