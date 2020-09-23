using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingProfile : Profile
    {
        public CreateUserRoleMappingProfile()
        {
            CreateMap<CreateUserRoleMappingCommand, UserRoleMappingEntity>();
        }
    }
}
