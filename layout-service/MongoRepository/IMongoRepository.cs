using MongoDB.Driver;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mongo
{
    public interface IMongoRepository<T> : IBaseRepository<T> where T : IBaseEntity
    {

    }
}
