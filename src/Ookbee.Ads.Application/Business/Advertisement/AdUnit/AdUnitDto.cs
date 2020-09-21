using Ookbee.Ads.Application.Business.Advertisement.AdGroup;
using Ookbee.Ads.Application.Business.Advertisement.AdUnitType;
using Ookbee.Ads.Application.Business.Advertisement.Publisher;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit
{
    public class AdUnitDto : DefaultDto
    {
        public AdGroupDto AdGroup { get; set; }
        public AdNetworkDto AdNetwork { get; set; }
        public int? SortSeq { get; set; }

        public static Expression<Func<AdUnitEntity, AdUnitDto>> Projection
        {
            get
            {
                return entity => new AdUnitDto()
                {
                    Id = entity.Id,
                    AdGroup = new AdGroupDto()
                    {
                        Id = entity.AdGroup.Id,
                        Name = entity.AdGroup.Name,
                        Description = entity.AdGroup.Description,
                        AdUnitType = new AdUnitTypeDto()
                        {
                            Id = entity.AdGroup.AdUnitType.Id,
                            Name = entity.AdGroup.AdUnitType.Name,
                            Description = entity.AdGroup.AdUnitType.Description
                        },
                        Publisher = new PublisherDto()
                        {
                            Id = entity.AdGroup.Publisher.Id,
                            Name = entity.AdGroup.Publisher.Name,
                            Description = entity.AdGroup.Publisher.Description
                        }
                    },
                    AdNetwork = new AdNetworkDto()
                    {
                        Name = entity.AdNetwork,
                        UnitIds = entity.AdNetworks
                            .Where(item => item.DeletedAt == null)
                            .Select(x => new AdUnitNetworkUnitIdDto()
                            {
                                Id = x.Id,
                                Platform = x.Platform,
                                AdNetworkUnitId = x.AdNetworkUnitId
                            })
                    },
                    SortSeq = entity.SortSeq,
                };
            }
        }
    }
}