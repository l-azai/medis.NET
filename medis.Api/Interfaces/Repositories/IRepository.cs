using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync();

        Task<T> GetByIdAsync(ObjectId id);

        Task AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> RemoveAsync(ObjectId id);
    }
}
