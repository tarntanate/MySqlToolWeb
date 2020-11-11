using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad
{
    public class AdDtoProfile : Profile
    {
        public AdDtoProfile()
        {
            CreateMap<AdEntity, AdDto>();
        }
    }
}
