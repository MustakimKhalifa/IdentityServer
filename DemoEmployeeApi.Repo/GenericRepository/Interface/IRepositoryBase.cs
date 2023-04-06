using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Repo.GenericRepository.Interface
{
    public interface IRepositoryBase<T> 
    {
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        void Insert(T? entity);
        void Update(T? entity);      
        void Remove(T? entity);
        void SaveChanges();
    }
}
