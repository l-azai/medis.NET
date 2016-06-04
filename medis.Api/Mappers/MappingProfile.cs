using AutoMapper;
using medis.Api.Models.Videos;
using medis.Api.ViewModel;

namespace medis.Api.Mappers
{
    public class MappingProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<VideoCategory, VideoCategoryViewModel>();
        }
    }
}