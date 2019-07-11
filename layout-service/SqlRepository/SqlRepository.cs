using Repository.Core;
using System;

namespace Repository.Sql
{
    public class SqlRepository<T> : BaseRepository<T>, ISqlRepository<T> where T : IBaseEntity
    {
        public SqlRepository(string tableName, ISqlDbContext sqlDbContext) : base(sqlDbContext)
        {

        }
    }
}
