using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAsync(Func<T, bool> condition = null);
        Task<T> SaveAsync(T data);
    }
}
