using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitMappingProfile : Profile
    {
        public CreateAdUnitMappingProfile()
        {
            CreateMap<CreateAdUnitCommand, AdUnitEntity>();
        }
    }
}
