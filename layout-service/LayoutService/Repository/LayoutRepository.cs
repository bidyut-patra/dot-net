using Microsoft.Extensions.Configuration;
using Repository.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layout.Service
{
    public class LayoutRepository : MongoRepository<Page>, ILayoutRepository
    {
        public LayoutRepository(ILayoutDbContext layoutDbContext, IConfiguration configuration) : base("LayoutCollection", configuration, layoutDbContext)
        {

        }
    }
}
