using Microsoft.Extensions.Configuration;
using Repository.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layout.Service
{
    public class LayoutDbContext : MongoDbContext, ILayoutDbContext
    {
        public LayoutDbContext(IConfiguration configuration): base("LayoutDb", configuration)
        {

        }
    }
}
