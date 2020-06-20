using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitMappingProfile : Profile
    {
        public UpdateAdUnitMappingProfile()
        {
            CreateMap<UpdateAdUnitCommand, AdUnitEntity>();
        }
    }
}
