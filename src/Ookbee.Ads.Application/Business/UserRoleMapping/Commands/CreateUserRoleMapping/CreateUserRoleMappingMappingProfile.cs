using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingMappingProfile : Profile
    {
        public CreateUserRoleMappingMappingProfile()
        {
            CreateMap<CreateUserRoleMappingCommand, UserRoleMappingEntity>();
        }
    }
}
