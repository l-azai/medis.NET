using medis.Api.Interfaces.Managers;
using System;
using System.Collections.Generic;
using medis.Api.Models.Videos;
using medis.Api.Interfaces.Repositories.Videos;
using medis.Api.Models.Shared;
using System.Linq;

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
        public IEnumerable<VideoCategory> GetAllVideoCategories()
        {
            //return _videoCatRepos.GetAll();
            return null;
        }

        /// <summary>
        /// Gets all videos.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VideoFile> GetAllVideos()
        {
            //return _videoRepos.GetAll();
            return null;
        }

        /// <summary>
        /// Gets the video by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public VideoFile GetVideoById(int id)
        {
            //return _videoRepos.GetById(id);
            return null;
        }

        /// <summary>
        /// Gets the video category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public VideoCategory GetVideoCategoryById(int id)
        {
            //return _videoCatRepos.GetById(id);

            return null;
        }

        /// <summary>
        /// Gets the videos by category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public IEnumerable<VideoFile> GetVideosByCategory(string category)
        {
            return _videoRepos.GetByCategory(category);
        }

        /// <summary>
        /// Gets the name of the video list by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IEnumerable<DropDownListItem> GetVideoListByName(string name)
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
        public VideoSearchResults GetVideoPagedResults(VideoSearchModel searchModel)
        {
            return _videoRepos.GetPagedResults(searchModel ?? new VideoSearchModel());
        }
    }
}