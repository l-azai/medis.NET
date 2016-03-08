using medis.Api.Models.Videos;
using medis.Api.Interfaces.Repositories.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace medis.Api.Repositories.Videos
{
    public class VideoRepository : Repository<VideoFile>, IVideoRepository
    {
        /// <summary>
        /// Gets the by category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public async Task<IList<VideoFile>> GetByCategory(string category)
        {
            var filter = Builders<VideoFile>.Filter.Eq(x => x.CategoryName, category);

            return await Collection.Find(filter)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<IList<VideoFile>> GetByName(string name)
        {
            return await Collection.AsQueryable()
                .Where(x => x.Name.ToLower().Contains(name))
                .ToListAsync();
        }

        public async Task<VideoSearchResults> GetPagedResults(VideoSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
