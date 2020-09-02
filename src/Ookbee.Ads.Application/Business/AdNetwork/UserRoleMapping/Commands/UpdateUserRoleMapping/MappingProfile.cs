using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateUserRoleMappingCommand, UserRoleMappingEntity>();
        }
    }
}
