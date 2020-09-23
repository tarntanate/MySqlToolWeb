using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleMappingProfile : Profile
    {
        public CreateUserRoleMappingProfile()
        {
            CreateMap<CreateUserRoleCommand, UserRoleEntity>();
        }
    }
}
