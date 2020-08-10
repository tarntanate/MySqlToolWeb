using AutoMapper;

namespace Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Queries.GetAdGroupItemListByKey
{
    public class GetGroupItemListByKeyMappingProfile : Profile
    {
        public GetGroupItemListByKeyMappingProfile()
        {
            CreateMap<Business.AdGroupItem.AdGroupItemDto, GroupItemUnitDto>()
                .ForMember(dest => dest.AdNetwork, m => m.MapFrom(src => src.Name))
                .ForMember(dest => dest.AdUnitId, m => m.MapFrom(src => src.AdUnitKey));
        }
    }
}
