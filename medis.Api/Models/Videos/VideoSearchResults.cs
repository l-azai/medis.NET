using System.Collections.Generic;

namespace medis.Api.Models.Videos
{
    public class VideoSearchResults
    {
        public IEnumerable<VideoFile> PagedResults { get; set; }
        public int TotalRecords { get; set; }
        public int Page { get; set; }
    }
}