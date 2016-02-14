using log4net;
using medis.Api.Interfaces.Managers;
using medis.Api.Models.Videos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace medis.Api.Controllers
{
    [RoutePrefix("api/videos")]
    public class VideoController : ApiController
    {
        private IVideoManager _videoManager;
        private readonly ILog log = LogManager.GetLogger(typeof(VideoController));

        public VideoController(IVideoManager videoManager)
        {
            _videoManager = videoManager;
        }

        [Route("GetVideoCategoryList")]
        public async Task<IHttpActionResult> GetVideoCategories()
        {
            try
            {
                var categories = await _videoManager.GetAllVideoCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }
        }

        [Route("GetVideosByCategory/{category}")]
        public IHttpActionResult GetVideosByCategory(string category)
        {
            try
            {
                var videos = _videoManager.GetVideosByCategory(category);

                return Ok(videos);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }
            
        }

        [Route("GetVideoById/{id}")]
        public IHttpActionResult GetVideoById(string id)
        {
            var video = _videoManager.GetVideoById(id);

            return Ok(video);
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search([FromUri] VideoSearchModel searchModel = null)
        {
            var videos = _videoManager.GetVideoPagedResults(searchModel);

            return Ok(videos);
        }

        [Route("GetVideosByName/{name}")]
        public IHttpActionResult GetVideosByName(string name)
        {
            var list = _videoManager.GetVideosByName(name);

            return Ok(list);
        }
    }
}
