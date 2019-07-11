using Repository.Core;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Sql
{
    public class SqlEntityCollection<T> : BaseEntityCollection<T>, ISqlEntityCollection<T> where T : BaseEntity<T>
    {
        public DbSet<T> Collection { get; private set; }

        public SqlEntityCollection(ISqlDbContext mongoDbContext, string name) : base(mongoDbContext, name)
        {
            
        }

        public override async Task<List<T>> FindAsync(Expression<Func<T, bool>> condition = null)
        {
            if (condition == null)
            {
                return await this.Collection.ToListAsync<T>();
            }
            else
            {
                return await this.Collection.ToListAsync();
            }
        }

        //public override async Task<T> InsertAsync(T entity)
        //{
        //    await this.Collection.InsertOneAsync(entity);
        //    return entity;
        //}

        //public override async Task<List<T>> InsertManyAsync(List<T> entities)
        //{
        //    await this.Collection.InsertManyAsync(entities);
        //    return entities;
        //}

        //public override async Task<bool> ReplaceOneAsync(T entity, Expression<Func<T, bool>> condition)
        //{
        //    var result = await this.Collection.ReplaceOneAsync(condition, entity);
        //    return result.IsAcknowledged;
        //}
    }
}
