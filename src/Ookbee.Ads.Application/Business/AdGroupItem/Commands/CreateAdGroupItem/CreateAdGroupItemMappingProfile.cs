using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.CreateAdGroupItem
{
    public class CreateAdGroupItemMappingProfile : Profile
    {
        public CreateAdGroupItemMappingProfile()
        {
            CreateMap<CreateAdGroupItemCommand, AdGroupItemEntity>();
        }
    }
}
