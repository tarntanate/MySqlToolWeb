using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System.Linq;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser
{
    public class AdvertiserDtoProfile : Profile
    {
        public AdvertiserDtoProfile()
        {
            CreateMap<AdvertiserEntity, AdvertiserDto>()
                .ForMember(d => d.TotalCampaign, opts => opts.MapFrom(s => s.Campaigns.Where(x => x.DeletedAt == null).Count()));
        }
    }
}
