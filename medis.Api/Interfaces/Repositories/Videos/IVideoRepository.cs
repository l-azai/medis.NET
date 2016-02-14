using medis.Api.Models.Videos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Repositories.Videos
{
    public interface IVideoRepository : IRepository<VideoFile>
    {
        Task<IList<VideoFile>> GetByCategory(string category);

        Task<IList<VideoFile>> GetByName(string name);

        Task<VideoSearchResults> GetPagedResults(VideoSearchModel searchModel);
    }
}
