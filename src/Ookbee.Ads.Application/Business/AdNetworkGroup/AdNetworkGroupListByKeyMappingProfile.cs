using AutoMapper;
using Ookbee.Ads.Application.Business.AdUnit;

namespace Ookbee.Ads.Application.Business.AdNetworkGroup.Queries.GetAdAdNetworkGroupListByKey
{
    public class AdNetworkGroupListByKeyMappingProfile : Profile
    {
        public AdNetworkGroupListByKeyMappingProfile()
        {
            CreateMap<AdUnitDto, GroupUnitDto>()
                .ForMember(dest => dest.AdNetwork, m => m.MapFrom(src => src.AdNetwork))
                .ForMember(dest => dest.AdNetworkUnitId, m => m.MapFrom(src => src.AdNetworkUnitId));
        }
    }
}
