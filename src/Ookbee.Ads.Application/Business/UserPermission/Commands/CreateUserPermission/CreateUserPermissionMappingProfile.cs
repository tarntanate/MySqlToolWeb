using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionMappingProfile : Profile
    {
        public CreateUserPermissionMappingProfile()
        {
            CreateMap<CreateUserPermissionCommand, UserPermissionEntity>();
        }
    }
}
