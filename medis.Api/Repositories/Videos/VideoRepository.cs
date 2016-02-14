using medis.Api.Models.Videos;
using medis.Api.Interfaces.Repositories.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace medis.Api.Repositories.Videos
{
    public class VideoRepository : Repository<VideoFile>, IVideoRepository
    {
        public async Task<IList<VideoFile>> GetByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VideoFile>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<VideoSearchResults> GetPagedResults(VideoSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
