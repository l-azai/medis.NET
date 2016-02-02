using medis.Api.Models.Shared;
using medis.Api.Models.Videos;
using System.Collections.Generic;

namespace medis.Api.Interfaces.Managers
{
    public interface IVideoManager
    {
        IEnumerable<VideoCategory> GetAllVideoCategories();

        VideoCategory GetVideoCategoryById(int id);

        IEnumerable<VideoFile> GetAllVideos();

        VideoFile GetVideoById(int id);

        IEnumerable<VideoFile> GetVideosByCategory(string category);

        IEnumerable<DropDownListItem> GetVideoListByName(string name);

        VideoSearchResults GetVideoPagedResults(VideoSearchModel searchModel);
    }
}
