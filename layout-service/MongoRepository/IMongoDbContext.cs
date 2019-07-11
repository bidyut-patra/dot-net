using MongoDB.Driver;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mongo
{
    public interface IMongoDbContext : IBaseDbContext
    {
        IMongoDatabase Database { get; }
    }
}
