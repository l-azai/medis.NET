using DapperExtensions.Mapper;
using medis.Api.Models.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medis.Api.Mappers
{
    public class VideoMapper : ClassMapper<VideoFile>
    {
        public VideoMapper()
        {
            Map(x => x.Category).Ignore();
            Map(x => x.Image).Ignore();
            AutoMap();
        }
    }
}