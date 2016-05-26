using medis.Api.Interfaces.Managers;
using System;
using System.Collections.Generic;
using medis.Api.Models.Videos;
using medis.Api.Interfaces.Repositories.Videos;
using medis.Api.Models.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace medis.Api.Managers
{
    public class VideoManager : IVideoManager
    {
        private IVideoRepository _videoRepos;
        private IVideoCategoryRepository _videoCatRepos;

        public VideoManager(IVideoCategoryRepository videoCatRepos, IVideoRepository videoRepos)
        {
            _videoRepos = videoRepos;
            _videoCatRepos = videoCatRepos;
        }

        public async Task<IList<VideoCategory>> GetAllVideoCategories()
        {
            return await _videoCatRepos.GetAllAsync(); ;
        }

        public async Task<IList<VideoFile>> GetAllVideos()
        {
            return await _videoRepos.GetAllAsync();
        }

        public async Task<VideoFile> GetVideoById(string id)
        {
            return await _videoRepos.GetByIdAsync(id);
        }

        public async Task<VideoCategory> GetVideoCategoryById(string id)
        {
            return await _videoCatRepos.GetByIdAsync(id);
        }

        public async Task<IList<VideoFile>> GetVideosByCategory(string category)
        {
            return await _videoRepos.GetByCategoryAsync(category);
        }

        public Task<IList<DropDownListItem>> GetVideosByName(string name)
        {
            //var videos = _videoRepos.GetByName(name)?
            //    .ToList()
            //    .Select(x => new DropDownListItem { id = x.Id, name = x.Name });

            //return videos;

            return null;
        }

        public Task<VideoSearchResults> GetVideoPagedResults(VideoSearchModel searchModel)
        {
            //return _videoRepos.GetPagedResults(searchModel ?? new VideoSearchModel());
            return null;
        }
    }
}