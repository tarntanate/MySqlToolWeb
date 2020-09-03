using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeMappingProfile : Profile
    {
        public CreateAdUnitTypeMappingProfile()
        {
            CreateMap<CreateAdUnitTypeCommand, AdUnitTypeEntity>();
        }
    }
}
