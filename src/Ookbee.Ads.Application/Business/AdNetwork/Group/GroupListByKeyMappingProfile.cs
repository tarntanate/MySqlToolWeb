using AutoMapper;
using Ookbee.Ads.Application.Business.AdUnit;

namespace Ookbee.Ads.Application.Business.AdNetwork.Group.Queries.GetAdGroupListByKey
{
    public class GroupListByKeyMappingProfile : Profile
    {
        public GroupListByKeyMappingProfile()
        {
            CreateMap<AdUnitDto, GroupUnitDto>()
                .ForMember(dest => dest.AdNetwork, m => m.MapFrom(src => src.AdNetwork))
                .ForMember(dest => dest.AdNetworkUnitId, m => m.MapFrom(src => src.AdNetworkUnitId));
        }
    }
}
