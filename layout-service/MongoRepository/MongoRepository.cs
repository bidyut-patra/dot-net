using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Repository.Core;
using System;

namespace Repository.Mongo
{
    public class MongoRepository<T> : BaseRepository<T>, IMongoRepository<T> where T : IBaseEntity
    {
        public MongoRepository(string collection, IConfiguration configuration, IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
            var mongoColectionName = configuration.GetSection("MongoConfiguration")[collection];
            // Initialize
            InitializeConventionPack();
            this.InitializeCollection(mongoDbContext, mongoColectionName);
        }

        /// <summary>
        /// Intialize all the settings here
        /// </summary>
        private void Initialize()
        {

        }

        /// <summary>
        /// Initializes the collection
        /// </summary>
        private void InitializeCollection(IMongoDbContext dbContext, string collection)
        {
            this.Table = new MongoEntityCollection<T>(dbContext, collection);
        }

        /// <summary>
        /// Initializes the convention pack
        /// </summary>
        private static void InitializeConventionPack()
        {
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            // Apply this convention pack for all the entities
            ConventionRegistry.Register("CamelCaseConvention", conventionPack, type => true);
        }
    }
}
