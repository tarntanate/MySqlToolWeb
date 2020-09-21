using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingProfile : Profile
    {
        public CreateUserRoleMappingProfile()
        {
            CreateMap<CreateUserRoleMappingCommand, UserRoleMappingEntity>();
        }
    }
}