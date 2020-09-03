using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionMappingProfile : Profile
    {
        public UpdateUserPermissionMappingProfile()
        {
            CreateMap<UpdateUserPermissionCommand, UserPermissionEntity>();
        }
    }
}
