using AutoMapper;
using Ookbee.Ads.Application.Business.AdUnit;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache
{
    public class CreateAdUnitCacheMappingProfile : Profile
    {
        public CreateAdUnitCacheMappingProfile()
        {
            CreateMap<AdUnitDto, AdUnitCacheDto>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.AdNetworkUnitId))
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.AdNetwork));
        }
    }
}
