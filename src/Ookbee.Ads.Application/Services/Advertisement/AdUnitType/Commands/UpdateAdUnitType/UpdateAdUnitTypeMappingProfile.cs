using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeMappingProfile : Profile
    {
        public UpdateAdUnitTypeMappingProfile()
        {
            CreateMap<UpdateAdUnitTypeCommand, AdUnitTypeEntity>();
        }
    }
}
