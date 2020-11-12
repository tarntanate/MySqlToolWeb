using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System.Linq;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign
{
    public class CampaignDtoProfile : Profile
    {
        public CampaignDtoProfile()
        {
            CreateMap<CampaignEntity, CampaignDto>()
                .ForMember(d => d.TotalAds, opts => opts.MapFrom(s => s.Ads.Where(x => x.DeletedAt == null).Count()));
        }
    }
}
