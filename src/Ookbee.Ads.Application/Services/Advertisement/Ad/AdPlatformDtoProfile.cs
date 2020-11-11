using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad
{
    public class AdPlatformDtoProfile : Profile
    {
        public AdPlatformDtoProfile()
        {
            CreateMap<AdEntity, AdPlatformDto>();
        }
    }
}