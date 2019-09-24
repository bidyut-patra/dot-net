using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace sample.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
    {
        public string Repository { get; set; }
        public string ConnectionString { get; set; }
        public IMongoCollection<T> Collection { get; set; }

        public GenericRepository(string connectionName)
        {
            this.InitializeRepo(connectionName);
        }

        private void InitializeRepo(string connectionName)
        {
            this.Repository = ConfigurationManager.AppSettings[connectionName].ToString();
            this.ConnectionString = ConfigurationManager.ConnectionStrings["mongo"].ToString();
            var connection = new MongoClient(this.ConnectionString);
            var database = connection.GetDatabase(this.Repository);
            this.Collection = database.GetCollection<T>(this.Repository);
        }

        public async Task<List<T>> GetAsync(Func<T, bool> condition = null)
        {
            if(condition == null)
            {
                var result = await Task.Run(() => this.Collection.AsQueryable<T>());
                return result.ToList();
            }
            else
            {
                var result = await Task.Run(() => this.Collection.AsQueryable<T>().Where(condition));
                return result.ToList();
            }
        }

        public async Task<T> SaveAsync(T data)
        {
            await this.Collection.InsertOneAsync(data);
            return data;
        }
    }
}