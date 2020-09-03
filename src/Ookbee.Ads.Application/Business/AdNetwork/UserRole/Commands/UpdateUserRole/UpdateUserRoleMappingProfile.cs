using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleMappingProfile : Profile
    {
        public UpdateUserRoleMappingProfile()
        {
            CreateMap<UpdateUserRoleCommand, UserRoleEntity>();
        }
    }
}
