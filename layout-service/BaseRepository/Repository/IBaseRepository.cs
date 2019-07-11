using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        IBaseEntityCollection<T> Table { get; }
        IBaseDbContext DbContext { get; }

        Task<List<T>> GetAsync(Expression<Func<T, bool>> condition = null);
        Task<T> GetOneAsync(Expression<Func<T, bool>> condition);
        Task<bool> UpdateAsync(T entity, Expression<Func<T, bool>> condition);
        Task<T> InsertAsync(T entity);
        Task<List<T>> InsertManyAsync(List<T> entities);
    }
}
