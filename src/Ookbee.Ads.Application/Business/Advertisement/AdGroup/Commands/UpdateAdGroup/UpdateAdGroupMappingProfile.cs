using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupMappingProfile : Profile
    {
        public UpdateAdGroupMappingProfile()
        {
            CreateMap<UpdateAdGroupCommand, AdGroupEntity>();
        }
    }
}
