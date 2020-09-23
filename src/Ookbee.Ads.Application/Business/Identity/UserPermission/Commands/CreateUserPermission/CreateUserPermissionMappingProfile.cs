using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Identity.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionMappingProfile : Profile
    {
        public CreateUserPermissionMappingProfile()
        {
            CreateMap<CreateUserPermissionCommand, UserPermissionEntity>();
        }
    }
}
