using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.CreateAdNetwork
{
    public class CreateAdNetworkMappingProfile : Profile
    {
        public CreateAdNetworkMappingProfile()
        {
            CreateMap<CreateAdNetworkCommand, AdNetworkEntity>();
        }
    }
}
