using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingProfile : Profile
    {
        public UpdateUserRoleMappingProfile()
        {
            CreateMap<UpdateUserRoleMappingCommand, UserRoleMappingEntity>();
        }
    }
}
