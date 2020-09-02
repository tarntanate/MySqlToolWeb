using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.User.Commands.UpdateUser
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateUserCommand, UserEntity>();
        }
    }
}
