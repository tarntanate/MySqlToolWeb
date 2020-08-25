using AutoMapper;
using Ookbee.Ads.Application.Business.AdUnit;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache
{
    public class CreateAdUnitCacheMappingProfile : Profile
    {
        public CreateAdUnitCacheMappingProfile()
        {
            CreateMap<AdUnitDto, AdUnitCacheDto>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dest => dest.AdNetwork, m => m.MapFrom(src => src.AdNetwork))
                .ForMember(dest => dest.AdNetworkUnitId, m => m.MapFrom(src => src.AdNetworkUnitId));
        }
    }
}
