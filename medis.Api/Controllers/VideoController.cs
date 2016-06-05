using AutoMapper;
using log4net;
using medis.Api.Interfaces.Managers;
using medis.Api.Models.Videos;
using medis.Api.ViewModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace medis.Api.Controllers
{
    [RoutePrefix("api/videos")]
    public class VideoController : ApiController
    {
        private readonly IVideoManager _videoManager;
        private readonly IMapper _mapper;
        private readonly ILog log = LogManager.GetLogger(typeof(VideoController));

        public VideoController(IVideoManager videoManager, IMapper mapper)
        {
            _videoManager = videoManager;
            _mapper = mapper;
        }

        [Route("GetVideoCategoryList")]
        public async Task<IHttpActionResult> GetVideoCategories()
        {
            try
            {
                var categories = await _videoManager.GetAllVideoCategories();
                var model = _mapper.Map<IList<VideoCategory>, IList<VideoCategoryViewModel>>(categories);

                foreach (var m in model) {
                    var video = await _videoManager.GetLatestVideoByCategoryName(m.Name);
                    m.ImageGfsFilename = video?.ImageGfsFilename;
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }
        }

        [Route("GetVideosByCategory/{category}")]
        public async Task<IHttpActionResult> GetVideosByCategory(string category)
        {
            try
            {
                var videos = await _videoManager.GetVideosByCategory(category);
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
            try {
                ObjectId videoId;

                if (ObjectId.TryParse(id, out videoId))
                {
                    var video = _videoManager.GetVideoById(ObjectId.Parse(id));
                    return Ok(video);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }
            
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
