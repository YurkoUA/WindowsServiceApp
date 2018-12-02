using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WindowsServiceApp.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entities);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task DeleteAllAsync();
    }
}
