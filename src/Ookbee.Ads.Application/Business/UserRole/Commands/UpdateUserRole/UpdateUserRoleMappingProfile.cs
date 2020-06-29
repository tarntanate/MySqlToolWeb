using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleMappingProfile : Profile
    {
        public UpdateUserRoleMappingProfile()
        {
            CreateMap<UpdateUserRoleCommand, UserRoleEntity>();
        }
    }
}
