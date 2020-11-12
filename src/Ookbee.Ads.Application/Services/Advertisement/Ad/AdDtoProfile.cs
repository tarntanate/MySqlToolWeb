using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad
{
    public class AdDtoProfile : Profile
    {
        public AdDtoProfile()
        {
            CreateMap<AdEntity, AdDto>()
                .ForMember(d => d.LinkUrl, opt => opt.MapFrom(s => s.WebLink))
                .ForMember(d => d.Assets, opt => opt.MapFrom(s => s.AdAssets));
        }
    }
}
