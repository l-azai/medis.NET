using medis.Api.Models.Shared;
using medis.Api.Models.Videos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Managers
{
    public interface IVideoManager
    {
        Task<IList<VideoCategory>> GetAllVideoCategories();

        Task<VideoCategory> GetVideoCategoryById(int id);

        Task<IList<VideoFile>> GetAllVideos();

        Task<VideoFile> GetVideoById(string id);

        Task<IList<VideoFile>> GetVideosByCategory(string category);

        Task<IList<DropDownListItem>> GetVideosByName(string name);

        Task<VideoSearchResults> GetVideoPagedResults(VideoSearchModel searchModel);

        Task<VideoFile> AddVideoFile(VideoFile video);
    }
}
