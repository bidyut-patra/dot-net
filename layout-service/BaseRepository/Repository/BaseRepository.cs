using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : IBaseEntity
    {
        public IBaseEntityCollection<T> Table { get; protected set; }
        public IBaseDbContext DbContext { get; internal set; }

        public BaseRepository(IBaseDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> condition = null)
        {
            if (condition == null)
            {
                return await this.Table.FindAsync();
            }
            else
            {
                return await this.Table.FindAsync(condition);
            }
        }

        public virtual async Task<T> GetOneAsync(Expression<Func<T, bool>> condition)
        {
            var entities = await this.Table.FindAsync(condition);
            if (entities.Count == 0)
            {
                return default(T);
            }
            else
            {
                return entities[0];
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity, Expression<Func<T, bool>> condition)
        {
            SetBaseAttributes(entity, false);
            return await this.Table.ReplaceOneAsync(entity, condition);
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            SetBaseAttributes(entity, true);
            await this.Table.InsertAsync(entity);
            return entity;
        }

        public virtual async Task<List<T>> InsertManyAsync(List<T> entities)
        {
            entities?.ForEach(entity => this.SetBaseAttributes(entity, true));
            await this.Table.InsertManyAsync(entities);
            return entities;
        }

        private void SetBaseAttributes(T entity, bool isNew)
        {
            var baseEntity = entity as BaseEntity<T>;
            if (isNew)
            {
                baseEntity.CreatedBy = "";
                baseEntity.CreatedDate = DateTime.Now;
            }
            baseEntity.ModifiedBy = "";
            baseEntity.ModifiedDate = DateTime.Now;
        }

        public void Dispose()
        {

        }
    }
}
