namespace medis.Api.Models.Videos
{
    public class VideoCategory : Entity
    {
        public string Name { get; set; }
        public string UrlKey { get; set; }
        public int TypeId { get; set; }
    }
}