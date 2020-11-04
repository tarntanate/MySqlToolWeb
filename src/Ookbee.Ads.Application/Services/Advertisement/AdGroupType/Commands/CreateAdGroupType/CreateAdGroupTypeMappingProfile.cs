using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.CreateAdGroupType
{
    public class CreateAdGroupTypeMappingProfile : Profile
    {
        public CreateAdGroupTypeMappingProfile()
        {
            CreateMap<CreateAdGroupTypeCommand, AdGroupTypeEntity>();
        }
    }
}
