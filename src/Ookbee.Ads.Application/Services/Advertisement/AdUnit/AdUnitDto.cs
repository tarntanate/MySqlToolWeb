using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit
{
    public class AdUnitDto : DefaultDto
    {
        public AdGroupDto AdGroup { get; set; }
        public AdUnitNetworkDto AdNetwork { get; set; }
        public int? SortSeq { get; set; }

        public static AdUnitDto FromEntity(AdUnitEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<AdUnitEntity, AdUnitDto>> Projection
        {
            get
            {
                return entity => new AdUnitDto()
                {
                    Id = entity.Id,
                    AdGroup = AdGroupDto.FromEntity(entity.AdGroup),
                    AdNetwork = new AdUnitNetworkDto()
                    {
                        Name = entity.AdNetwork,
                        AdNetworkUnits = entity.AdNetworks != null ? entity.AdNetworks
                            .AsQueryable()
                            .Where(adNetwork => adNetwork.DeletedAt == null)
                            .Select(AdUnitNetworkUnitIdDto.Projection) : null
                    },
                    SortSeq = entity.SortSeq,
                };
            }
        }
    }
}