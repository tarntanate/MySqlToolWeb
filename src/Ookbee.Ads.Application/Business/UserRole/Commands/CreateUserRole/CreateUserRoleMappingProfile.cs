using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleMappingProfile : Profile
    {
        public CreateUserRoleMappingProfile()
        {
            CreateMap<CreateUserRoleCommand, UserRoleEntity>();
        }
    }
}
