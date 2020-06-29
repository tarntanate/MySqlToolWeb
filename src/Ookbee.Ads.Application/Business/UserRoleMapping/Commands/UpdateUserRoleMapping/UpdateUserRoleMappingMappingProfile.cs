using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingMappingProfile : Profile
    {
        public UpdateUserRoleMappingMappingProfile()
        {
            CreateMap<UpdateUserRoleMappingCommand, UserRoleMappingEntity>();
        }
    }
}
