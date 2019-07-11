using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mongo
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbContext(string dbName, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoConfiguration")["ConnectionString"];
            var mongoClient = new MongoClient(connectionString);
            var mongoDbName = configuration.GetSection("MongoConfiguration")[dbName];
            this.Database = mongoClient.GetDatabase(mongoDbName);
        }
    }
}
