using medis.Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Repositories
{
    public interface IPrimaryRepository<T>: IRepository<T> where T : PrimaryEntity
    {
        Task<T> GetByIdAsync(ObjectId id);

        Task<bool> RemoveAsync(ObjectId id);

        IFindFluent<T, T> GetMany(Expression<Func<T, bool>> query);
    }
}
