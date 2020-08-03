using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.UpdateAdGroupItem
{
    public class UpdateAdGroupItemMappingProfile : Profile
    {
        public UpdateAdGroupItemMappingProfile()
        {
            CreateMap<UpdateAdGroupItemCommand, AdGroupItemEntity>();
        }
    }
}
