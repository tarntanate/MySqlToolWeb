using AutoMapper;

namespace Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Queries.GetAdGroupItemListByKey
{
    public class GroupItemListByKeyMappingProfile : Profile
    {
        public GroupItemListByKeyMappingProfile()
        {
            CreateMap<Business.AdGroupItem.AdGroupItemDto, GroupItemUnitDto>()
                .ForMember(dest => dest.AdNetwork, m => m.MapFrom(src => src.Name))
                .ForMember(dest => dest.AdUnitId, m => m.MapFrom(src => src.AdUnitKey));
        }
    }
}
