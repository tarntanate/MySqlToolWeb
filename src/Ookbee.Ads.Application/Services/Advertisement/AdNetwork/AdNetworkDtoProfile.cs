using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork
{
    public class AdNetworkDtoProfile : Profile
    {
        public AdNetworkDtoProfile()
        {
            CreateMap<AdNetworkEntity, AdNetworkDtoProfile>();
        }
    }
}
