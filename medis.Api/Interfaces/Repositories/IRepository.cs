using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(string id);

        Task<T> GetById(ObjectId id);

        Task<T> Add(T entity);

        Task<bool> Update(T entity);

        Task<bool> Remove(string id);

        Task<bool> Remove(ObjectId id);
    }
}
