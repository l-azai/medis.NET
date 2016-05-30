using MongoDB.Bson;

namespace medis.Api.Models.Videos
{
    public class VideoFile : PrimaryEntity
    {
        public string Name { get; set; }
        
        public string NameUrl { get; set; }

        public int YearReleased { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ImageGfsFilename { get; set; }

        public string ImageFilename { get; set; }

        public string VideoGfsFilename { get; set; }

        public string VideoFilename { get; set; }

        public string Quality { get; set; }
    }
}