using Microsoft.EntityFrameworkCore;

namespace Repository.Sql
{
    public class SqlDbContext : DbContext, ISqlDbContext
    {        
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }
    }
}
