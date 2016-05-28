using medis.Api.Infrastructure;
using medis.Api.Interfaces.Repositories;
using medis.Api.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace medis.Api.Repositories
{
    public class SecondaryRepository<T> : ISecondaryRepository<T> where T : SecondaryEntity
    {
        protected IMongoCollection<T> Collection { 
            get {
                var db = DBInstance.Database;
                return db.GetCollection<T>(typeof(T).Name);
            }
        }

        public async Task AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Collection
                .Find(x => true)
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var result = await Collection
                .ReplaceOneAsync(x => x.Id == entity.Id, entity);
            
            return result.IsAcknowledged;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var deletedRecord = await Collection
                .DeleteOneAsync(x => x.Id == id);

            return deletedRecord.IsAcknowledged;
        }
    }
}