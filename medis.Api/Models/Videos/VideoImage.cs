namespace medis.Api.Models.Videos
{
    public class VideoImage : DapperEntity
    {
        public int VideoId { get; set; }

        public byte[] Content { get; set; }
    }
}