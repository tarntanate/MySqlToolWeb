using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkMappingProfile : Profile
    {
        public UpdateAdNetworkMappingProfile()
        {
            CreateMap<UpdateAdNetworkCommand, AdNetworkEntity>();
        }
    }
}
