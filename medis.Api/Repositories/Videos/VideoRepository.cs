using medis.Api.Models.Videos;
using medis.Api.Interfaces.Repositories.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace medis.Api.Repositories.Videos
{
    public class VideoRepository : Repository<VideoFile>, IVideoRepository
    {
        public IEnumerable<VideoFile> GetByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VideoFile> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public VideoSearchResults GetPagedResults(VideoSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
