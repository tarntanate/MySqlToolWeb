using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup
{
    public class AdGroupDtoProfile : Profile
    {
        public AdGroupDtoProfile()
        {
            CreateMap<AdGroupEntity, AdGroupDto>();
        }
    }
}
