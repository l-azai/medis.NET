﻿using medis.Api.Infrastructure;
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
        protected IMongoCollection<T> Collection { 
            get {
                var db = DBInstance.Database;
                return db.GetCollection<T>(typeof(T).Name);
            }
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);

            return entity;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<T>> GetAllAsync()
        {
            return await Collection
                .Find<T>(Builders<T>.Filter.Empty)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(string id)
        {
            return await Collection
                .Find(Builders<T>.Filter.Eq(x => x.Id, ObjectId.Parse(id)))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(ObjectId id)
        {
            return await Collection
                .Find(Builders<T>.Filter.Eq(x => x.Id, id))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            var result = await Collection
                .ReplaceOneAsync(Builders<T>.Filter.Eq(x => x.Id, entity.Id), entity);
            
            return result.IsAcknowledged;
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string id)
        {
            var deletedRecord = await Collection
                .DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, ObjectId.Parse(id)));
            
            return deletedRecord.IsAcknowledged;
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(ObjectId id)
        {
            var deletedRecord = await Collection
                .DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, id));

            return deletedRecord.IsAcknowledged;
        }
    }
}