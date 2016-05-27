using medis.Api.Infrastructure;
using medis.Api.Interfaces.Repositories;
using medis.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace medis.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected IMongoCollection<T> Collection { 
            get {
                var db = DBInstance.Database;
                return db.GetCollection<T>(typeof(T).Name);
            }
        }

        public async Task AddAsync(T entity)
        {
            entity.DateCreated = DateTime.Now;
            await Collection.InsertOneAsync(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Collection
                .Find(x => true)
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await Collection
                .Find(Builders<T>.Filter.Eq(x => x.Id, ObjectId.Parse(id)))
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(ObjectId id)
        {
            return await Collection
                .Find(Builders<T>.Filter.Eq(x => x.Id, id))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var result = await Collection
                .ReplaceOneAsync(Builders<T>.Filter.Eq(x => x.Id, entity.Id), entity);
            
            return result.IsAcknowledged;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var deletedRecord = await Collection
                .DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, ObjectId.Parse(id)));
            
            return deletedRecord.IsAcknowledged;
        }

        public async Task<bool> RemoveAsync(ObjectId id)
        {
            var deletedRecord = await Collection
                .DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, id));

            return deletedRecord.IsAcknowledged;
        }
    }
}