using medis.Api.Infrastructure;
using medis.Api.Interfaces.Repositories;
using medis.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson;

namespace medis.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected IMongoCollection<T> GetCollection() {
            var db = DBInstance.Database;
            return db.GetCollection<T>(typeof(T).Name);
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<T> Add(T entity)
        {
            var col = GetCollection();
            await col.InsertOneAsync(entity);

            return entity;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<T>> GetAll()
        {
            return await GetCollection()
                .Find<T>(Builders<T>.Filter.Empty)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetById(string id)
        {
            return await GetCollection()
                .Find(Builders<T>.Filter.Eq(x => x.Id, ObjectId.Parse(id)))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetById(ObjectId id)
        {
            return await GetCollection()
                .Find(Builders<T>.Filter.Eq(x => x.Id, id))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<bool> Update(T entity)
        {
            var result = await GetCollection()
                .ReplaceOneAsync(Builders<T>.Filter.Eq(x => x.Id, entity.Id), entity);
            
            return result.IsAcknowledged;
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> Remove(string id)
        {
            var deletedRecord = await GetCollection()
                .DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, ObjectId.Parse(id)));
            
            return deletedRecord.IsAcknowledged;
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> Remove(ObjectId id)
        {
            var deletedRecord = await GetCollection()
                .DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, id));

            return deletedRecord.IsAcknowledged;
        }
    }
}