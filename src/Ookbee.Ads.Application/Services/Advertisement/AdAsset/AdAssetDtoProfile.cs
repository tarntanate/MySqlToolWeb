using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset
{
    public class AdAssetDtoProfile : Profile
    {
        public AdAssetDtoProfile()
        {
            CreateMap<AdAssetEntity, AdAssetDto>();
        }
    }
}
