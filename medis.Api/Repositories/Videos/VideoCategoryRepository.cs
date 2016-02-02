using medis.Api.Interfaces.Repositories.Videos;
using medis.Api.Models.Videos;

namespace medis.Api.Repositories.Videos
{
    public class VideoCategoryRepository : Repository<VideoCategory>, IVideoCategoryRepository
    {
        public VideoCategoryRepository()
        {

        }
    }
}