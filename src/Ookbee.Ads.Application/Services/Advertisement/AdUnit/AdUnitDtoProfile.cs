using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System.Linq;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit
{
    public class AdUnitDtoProfile : Profile
    {
        public AdUnitDtoProfile()
        {
            CreateMap<AdUnitEntity, AdUnitDto>()
                .ForMember(d => d.AdNetwork, opt => opt.MapFrom(s => new AdUnitNetworkDto()
                {
                    Name = s.AdNetwork,
                    AdNetworkUnits = s.AdNetworks.Where(x => x.DeletedAt == null).Select(x => new AdUnitNetworkUnitIdDto()
                    {
                        Id = x.Id,
                        Platform = x.Platform,
                        AdNetworkUnitId = x.AdNetworkUnitId
                    })
                })
            );
        }
    }
}
