﻿using medis.Api.Infrastructure;
using medis.Api.Interfaces.Repositories;
using medis.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;

namespace medis.Api.Repositories
{
    public class PrimaryRepository<T> : IPrimaryRepository<T> where T : PrimaryEntity
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
            entity.DateUpdated = DateTime.Now;
            await Collection.InsertOneAsync(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Collection
                .Find(x => true)
                .ToListAsync();
        }

        public IFindFluent<T, T> GetMany(Expression<Func<T, bool>> query)
        {
            return Collection.Find(query);
        }

        public async Task<T> GetByIdAsync(ObjectId id)
        {
            return await Collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            entity.DateUpdated = DateTime.Now;

            var result = await Collection
                .ReplaceOneAsync(x => x.Id == entity.Id, entity);
            
            return result.IsAcknowledged;
        }

        public async Task<bool> RemoveAsync(ObjectId id)
        {
            var deletedRecord = await Collection
                .DeleteOneAsync(x => x.Id == id);

            return deletedRecord.IsAcknowledged;
        }
    }
}