using medis.Api.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace medis.Api.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private IVideoManager _videoManager;

        public AdminController(IVideoManager videoManager)
        {
            _videoManager = videoManager;
        }
    }
}
