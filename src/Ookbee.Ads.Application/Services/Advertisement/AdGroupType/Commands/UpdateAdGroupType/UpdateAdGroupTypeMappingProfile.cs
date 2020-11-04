using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.UpdateAdGroupType
{
    public class UpdateAdGroupTypeMappingProfile : Profile
    {
        public UpdateAdGroupTypeMappingProfile()
        {
            CreateMap<UpdateAdGroupTypeCommand, AdGroupTypeEntity>();
        }
    }
}
