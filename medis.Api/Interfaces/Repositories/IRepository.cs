using System.Collections.Generic;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync();

        Task AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);
    }
}
