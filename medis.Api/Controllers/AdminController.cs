using log4net;
using medis.Api.Enums;
using medis.Api.Extensions;
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
        private readonly ILog Log = LogManager.GetLogger(typeof(AdminController));

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
            await Request.Content.ReadAsMultipartAsync(provider);

            if (!provider.FileData.Any())
            {
                return BadRequest("Image file needs to be uploaded");
            }
            var file = provider.FileData.First();

            try
            {
                var model = JsonConvert.DeserializeObject<AddVideoViewModel>(provider.FormData["model"]);

                var category = await _videoManager.GetVideoCategoryById(model.VideoCategoryId);

                if (category == null)
                {
                    return BadRequest();
                }
                
                var uniqueFilename = file.Headers.ContentDisposition.FileName.ToGfsFilename();
                var sanitizedFilename = file.Headers.ContentDisposition.FileName.SanitizeWebApiContentDispositionFilename();

                using (var fs = new FileStream(file.LocalFileName, FileMode.Open)) {
                    await _gridFsHelper.UploadFromStreamAsync(uniqueFilename, fs, sanitizedFilename, MimeMapping.GetMimeMapping(sanitizedFilename), MediaTypeEnum.Images);

                    var video = new VideoFile
                    {
                        CategoryId = model.VideoCategoryId,
                        CategoryName = category.Name,
                        Name = model.VideoFilename,
                        NameUrl = model.VideoFilename.Slugify(),
                        YearReleased = model.YearReleased,
                        Quality = model.Quality,
                        ImageGfsFilename = uniqueFilename,
                        ImageFilename = sanitizedFilename
                    };

                    await _videoManager.AddVideoFile(video);
                }

                File.Delete(file.LocalFileName);

                return Ok();
            }
            catch (Exception ex)
            {
                File.Delete(file.LocalFileName);
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
    }
}
