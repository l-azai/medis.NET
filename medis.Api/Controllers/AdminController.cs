using medis.Api.Enums;
using medis.Api.Interfaces.Helpers;
using medis.Api.Interfaces.Managers;
using medis.Api.Models.Videos;
using medis.Api.ViewModel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace medis.Api.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly IVideoManager _videoManager;
        private readonly IGridFsHelper _gridFsHelper;

        public AdminController(IVideoManager videoManager, IGridFsHelper gridFsHelper)
        {
            _videoManager = videoManager;
            _gridFsHelper = gridFsHelper;
        }

        [Route("addvideo")]
        [HttpPost]
        public async Task<IHttpActionResult> AddVideo()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Unsupported media type.");
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                if (!provider.FileData.Any()) {
                    return BadRequest("Image file needs to be uploaded");
                }

                var filepath = provider.FileData.Select(x => x.LocalFileName).First();
                var model = JsonConvert.DeserializeObject<AddVideoViewModel>(provider.FormData["model"]);

                using (var fs = new FileStream(filepath, FileMode.Open)) {

                    var id = await _gridFsHelper.UploadFromStreamAsync(model.VideoFilename, fs, MediaTypeEnum.Images);

                    var video = new VideoFile {
                        CategoryId = model.VideoCategoryId,
                        Name = model.VideoFilename,
                        YearReleased = model.YearReleased,
                        ImageFileId = id
                    };

                    await _videoManager.AddVideoFile(video);
                }
                
                File.Delete(filepath);
                
                return Ok();
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
