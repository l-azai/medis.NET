using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using medis.Api.Infrastructure;
using medis.Api.Interfaces.Repositories.Videos;
using medis.Api.Models.Videos;
using MongoDB.Driver;
using medis.Api.Interfaces.Repositories;

namespace medis.Api.Repositories.Videos
{
    public class VideoCategoryRepository: IVideoCategoryRepository
    {
        protected IMongoCollection<VideoCategory> Collection
        {
            get
            {
                var db = DBInstance.Database;
                return db.GetCollection<VideoCategory>(typeof(VideoCategory).Name);
            }
        }

        public Task<IList<VideoCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<VideoCategory> GetByIdAsync(int id)
        {
            return await Collection
                .Find(Builders<VideoCategory>.Filter.Eq(x => x.Id, id))
                .FirstOrDefaultAsync();
        }
    }
}