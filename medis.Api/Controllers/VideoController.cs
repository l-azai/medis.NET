using medis.Api.Interfaces.Managers;
using medis.Api.Models.Videos;
using System.Collections.Generic;
using System.Web.Http;

namespace medis.Api.Controllers
{
    [RoutePrefix("api/videos")]
    public class VideoController : ApiController
    {
        private IVideoManager _videoManager;

        public VideoController(IVideoManager videoManager)
        {
            _videoManager = videoManager;
        }

        [Route("GetVideoCategoryList")]
        public IHttpActionResult GetVideoCategories()
        {
            var categories = _videoManager.GetAllVideoCategories();

            return Ok(categories);
        }

        [Route("GetVideosByCategory/{category}")]
        public IHttpActionResult GetVideosByCategory(string category)
        {
            var videos = _videoManager.GetVideosByCategory(category);

            return Ok(videos);
        }

        [Route("GetVideoById/{id:int}")]
        public IHttpActionResult GetVideoById(int id)
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
            var list = _videoManager.GetVideoListByName(name);

            return Ok(list);
        }
    }
}
