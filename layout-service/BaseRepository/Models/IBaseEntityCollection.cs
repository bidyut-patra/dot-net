using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IBaseEntityCollection<T>
    {
        Task<List<T>> FindAsync(Expression<Func<T, bool>> condition = null);
        Task<T> InsertAsync(T entity);
        Task<List<T>> InsertManyAsync(List<T> entities);
        Task<bool> ReplaceOneAsync(T entity, Expression<Func<T, bool>> condition);
    }
}
