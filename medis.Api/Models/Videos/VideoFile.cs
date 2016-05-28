using MongoDB.Bson;

namespace medis.Api.Models.Videos
{
    public class VideoFile : PrimaryEntity
    {
        public string Name { get; set; }

        public int YearReleased { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public ObjectId ImageFileId { get; set; }

        public ObjectId? VideoFileId { get; set; }

        public string Quality { get; set; }
    }
}