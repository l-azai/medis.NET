using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medis.Api.Models.Videos
{
    public class VideoSearchModel
    {
        public string searchText { get; set; }
        public string sortName { get; set; }
        public bool? sortDescending { get; set; }
        public int? categoryFilterId { get; set; }
        public int pageSize { get; set; } = 10;
        public int skip { get; set; } = 0;
    }
}