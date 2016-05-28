using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using medis.Api.Infrastructure;
using medis.Api.Interfaces.Repositories.Videos;
using medis.Api.Models.Videos;
using MongoDB.Driver;
using medis.Api.Interfaces.Repositories;

namespace medis.Api.Repositories.Videos
{
    public class VideoCategoryRepository: SecondaryRepository<VideoCategory>, IVideoCategoryRepository
    {
    }
}