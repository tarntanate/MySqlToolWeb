using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionMappingProfile : Profile
    {
        public UpdateUserPermissionMappingProfile()
        {
            CreateMap<UpdateUserPermissionCommand, UserPermissionEntity>();
        }
    }
}
