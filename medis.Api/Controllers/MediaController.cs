﻿using medis.Api.Enums;
using medis.Api.Interfaces.Helpers;
using medis.Api.Interfaces.Managers;
using MongoDB.Bson;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace medis.Api.Controllers
{
    [RoutePrefix("api/media")]
    public class MediaController : ApiController
    {
        private readonly IGridFsHelper _gridFsHelper;
        private readonly IVideoManager _videoManager;

        public MediaController(IGridFsHelper gridFsHelper, IVideoManager videoManager)
        {
            _gridFsHelper = gridFsHelper;
            _videoManager = videoManager;
        }
        [HttpGet]
        [Route("image/{filename}")]
        public async Task<IHttpActionResult> ImageFile(string filename)
        {
            if (await _gridFsHelper.FileExistsAsync(filename, MediaTypeEnum.Images))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                using (var stream = await _gridFsHelper.OpenDownloadStreamByNameAsync(filename, MediaTypeEnum.Images)) {
                    var memStream = new MemoryStream();
                    stream.CopyTo(memStream);
                    memStream.Seek(0, SeekOrigin.Begin);

                    response.Content = new StreamContent(memStream);
                    response.Content.Headers.ContentLength = stream.Length;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(stream.FileInfo.Metadata["contentType"].ToString());
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = stream.FileInfo.Metadata["filename"].ToString()
                    };

                    await stream.CloseAsync();
                }

                return ResponseMessage(response);
            }
            else {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("image-by-video/{recordId}")]
        public async Task<IHttpActionResult> ImageByVideo(string recordId) {
            ObjectId objId;

            if (ObjectId.TryParse(recordId, out objId))
            {
                var videoFile = await _videoManager.GetVideoById(objId);

                if (videoFile == null) {
                    return BadRequest();
                }

                if (await _gridFsHelper.FileExistsAsync(videoFile.ImageGfsFilename, MediaTypeEnum.Images))
                {
                    var memStream = new MemoryStream();
                    await _gridFsHelper.DownloadToStreamByNameAsync(videoFile.ImageGfsFilename, memStream, MediaTypeEnum.Images);
                    memStream.Seek(0, SeekOrigin.Begin);

                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    
                    response.Content = new StreamContent(memStream);
                    response.Content.Headers.ContentLength = memStream.Length;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(videoFile.ImageFilename));
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = videoFile.ImageFilename
                    };

                    return ResponseMessage(response);
                }
                else {
                    return NotFound();
                }
            }
            else {
                return BadRequest();
            }
        }

        [Route("video/{fileId}")]
        public async Task<IHttpActionResult> VideoFile(string fileId)
        {
            ObjectId id;

            if (ObjectId.TryParse(fileId, out id))
            {
                var stream = new MemoryStream();

                await _gridFsHelper.DownloadToStreamAsync(id, stream, MediaTypeEnum.Images);

                var response = new HttpResponseMessage();
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment"); // application/octet-stream


                return ResponseMessage(response);
                //return response(HttpStatusCode.OK, response);
            }
            else {
                return BadRequest();
            }
        }

        //[HttpGet]
        //[Route("image/{recordId}/attachment")]
        //public async Task<IHttpActionResult> ImageAttachment(string recordId)
        //{
        //    ObjectId objId;

        //    if (ObjectId.TryParse(recordId, out objId))
        //    {
        //        var videoFile = await _videoManager.GetVideoById(objId);

        //        if (videoFile == null)
        //        {
        //            return BadRequest();
        //        }

        //        if (await _gridFsHelper.FileExistsAsync(videoFile.ImageGfsFilename, MediaTypeEnum.Images))
        //        {
        //            var byteArr = await _gridFsHelper.DownloadAsBytesByNameAsync(videoFile.ImageGfsFilename, MediaTypeEnum.Images);
        //            var response = new HttpResponseMessage(HttpStatusCode.OK);
        //            response.Content = new ByteArrayContent(byteArr);
        //            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = videoFile.ImageFilename
        //            };
        //            response.Content.Headers.ContentType = new MediaTypeHeaderValue(
        //                MimeMapping.GetMimeMapping(videoFile.ImageFilename));

        //            return ResponseMessage(response);
        //        }
        //        else {
        //            return NotFound();
        //        }
        //    }
        //    else {
        //        return BadRequest();
        //    }
        //}
    }
}