using Inventory_Management.Entities;
using System.Linq.Expressions;

namespace Inventory_Management.Common.Repositories
{
    public interface IRepository<T> where T : BaseModel 
    {
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
   
        T GetByID(int id);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
    }
}
