using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public class BaseEntityCollection<T> : IBaseEntityCollection<T>
    {
        public IBaseDbContext DbContext { get; }
        public string Name { get; }

        public BaseEntityCollection(IBaseDbContext dbContext, string name)
        {
            this.DbContext = dbContext;
            this.Name = name;
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> condition = null)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<T>> InsertManyAsync(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> ReplaceOneAsync(T entity, Expression<Func<T, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}
