using medis.Api.Models.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Repositories.Videos
{
    public interface IVideoCategoryRepository
    {
        Task<IList<VideoCategory>> GetAllAsync();
        Task<VideoCategory> GetByIdAsync(int id);
    }
}
