using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionMappingProfile : Profile
    {
        public UpdateUserPermissionMappingProfile()
        {
            CreateMap<UpdateUserPermissionCommand, UserPermissionEntity>();
        }
    }
}