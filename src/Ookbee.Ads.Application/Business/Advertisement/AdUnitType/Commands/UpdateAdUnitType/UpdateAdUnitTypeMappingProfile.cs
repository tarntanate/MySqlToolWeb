using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeMappingProfile : Profile
    {
        public UpdateAdUnitTypeMappingProfile()
        {
            CreateMap<UpdateAdUnitTypeCommand, AdUnitTypeEntity>();
        }
    }
}
