using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Commands.CreateAdGroup
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAdGroupCommand, AdGroupEntity>();
        }
    }
}
