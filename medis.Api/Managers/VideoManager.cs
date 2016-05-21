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

        /// <summary>
        /// Gets all video categories.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<VideoCategory>> GetAllVideoCategories()
        {
            return await _videoCatRepos.GetAllAsync(); ;
        }

        /// <summary>
        /// Gets all videos.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<VideoFile>> GetAllVideos()
        {
            return await _videoRepos.GetAllAsync();
        }

        /// <summary>
        /// Gets the video by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<VideoFile> GetVideoById(string id)
        {
            return await _videoRepos.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets the video category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<VideoCategory> GetVideoCategoryById(string id)
        {
            return await _videoCatRepos.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets the videos by category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public async Task<IList<VideoFile>> GetVideosByCategory(string category)
        {
            return await _videoRepos.GetByCategory(category);
        }

        /// <summary>
        /// Gets the name of the video list by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Task<IList<DropDownListItem>> GetVideosByName(string name)
        {
            //var videos = _videoRepos.GetByName(name)?
            //    .ToList()
            //    .Select(x => new DropDownListItem { id = x.Id, name = x.Name });

            //return videos;

            return null;
        }

        /// <summary>
        /// Gets the video paged results.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        public Task<VideoSearchResults> GetVideoPagedResults(VideoSearchModel searchModel)
        {
            //return _videoRepos.GetPagedResults(searchModel ?? new VideoSearchModel());
            return null;
        }
    }
}