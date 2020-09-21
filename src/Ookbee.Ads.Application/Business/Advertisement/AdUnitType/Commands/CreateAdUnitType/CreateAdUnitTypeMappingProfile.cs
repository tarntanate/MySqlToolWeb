using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeMappingProfile : Profile
    {
        public CreateAdUnitTypeMappingProfile()
        {
            CreateMap<CreateAdUnitTypeCommand, AdUnitTypeEntity>();
        }
    }
}
