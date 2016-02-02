using medis.Api.Models.Videos;
using System.Collections.Generic;

namespace medis.Api.Interfaces.Repositories.Videos
{
    public interface IVideoRepository : IRepository<VideoFile>
    {
        IEnumerable<VideoFile> GetByCategory(string category);

        IEnumerable<VideoFile> GetByName(string name);

        VideoSearchResults GetPagedResults(VideoSearchModel searchModel);
    }
}
