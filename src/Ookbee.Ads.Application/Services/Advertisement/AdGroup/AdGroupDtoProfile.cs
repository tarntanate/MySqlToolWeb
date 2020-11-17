using System.Linq;
using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup
{
    public class AdGroupDtoProfile : Profile
    {
        public AdGroupDtoProfile()
        {
            CreateMap<AdGroupEntity, AdGroupDto>()
                .ForMember(d => d.TotalAdUnit, opts => opts.MapFrom(s => s.AdUnits.Where(x => x.DeletedAt == null).Count()));;
        }
    }
}
