using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.User.Commands.CreateUser
{
    public class CreateUserMappingProfile : Profile
    {
        public CreateUserMappingProfile()
        {
            CreateMap<CreateUserCommand, UserEntity>();
        }
    }
}
