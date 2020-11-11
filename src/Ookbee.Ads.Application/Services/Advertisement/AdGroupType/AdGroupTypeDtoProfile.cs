
using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType
{
    public class AdGroupTypeDtoProfile : Profile
    {
        public AdGroupTypeDtoProfile()
        {
            CreateMap<AdGroupTypeEntity, AdGroupTypeDto>();
        }
    }
}
