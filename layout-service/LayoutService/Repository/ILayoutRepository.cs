using Repository.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layout.Service
{
    public interface ILayoutRepository : IMongoRepository<Page>
    {
    }
}
