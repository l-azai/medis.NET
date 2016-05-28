using medis.Api.Models.Videos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Repositories.Videos
{
    public interface IVideoRepository : IPrimaryRepository<VideoFile>
    {
        Task<IList<VideoFile>> GetByCategoryAsync(string category);

        Task<IList<VideoFile>> GetByNameAsync(string name);

        Task<VideoSearchResults> GetPagedResultsAsync(VideoSearchModel searchModel);
    }
}
