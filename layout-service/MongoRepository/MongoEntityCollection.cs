using MongoDB.Driver;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mongo
{
    public class MongoEntityCollection<T> : BaseEntityCollection<T>, IMongoEntityCollection<T>
    {
        public IMongoCollection<T> Collection { get; private set; }

        public MongoEntityCollection(IMongoDbContext mongoDbContext, string name) : base(mongoDbContext, name)
        {
            this.Collection = mongoDbContext.Database.GetCollection<T>(this.Name);
        }

        public override async Task<List<T>> FindAsync(Expression<Func<T, bool>> condition = null)
        {
            if (condition == null)
            {
                var result = await this.Collection.FindAsync<T>(FilterDefinition<T>.Empty);
                return result.ToList();
            }
            else
            {
                var result = await this.Collection.FindAsync<T>(condition);
                return result.ToList();
            }
        }

        public override async Task<T> InsertAsync(T entity)
        {
            await this.Collection.InsertOneAsync(entity);
            return entity;
        }

        public override async Task<List<T>> InsertManyAsync(List<T> entities)
        {
            await this.Collection.InsertManyAsync(entities);
            return entities;
        }

        public override async Task<bool> ReplaceOneAsync(T entity, Expression<Func<T, bool>> condition)
        {
            var result = await this.Collection.ReplaceOneAsync(condition, entity);
            return result.IsAcknowledged;
        }
    }
}
