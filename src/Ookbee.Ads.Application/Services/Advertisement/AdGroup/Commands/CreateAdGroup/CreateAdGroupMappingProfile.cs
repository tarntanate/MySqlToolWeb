using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupMappingProfile : Profile
    {
        public CreateAdGroupMappingProfile()
        {
            CreateMap<CreateAdGroupCommand, AdGroupEntity>();
        }
    }
}
