namespace medis.Api.Models.Videos
{
    public class VideoFile : Entity
    {
        public string Name { get; set; }

        public int YearReleased { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ImageGfsFilename { get; set; }

        public string VideoGfsFilename { get; set; }

        //public byte[] Content { get; set; }
    }
}