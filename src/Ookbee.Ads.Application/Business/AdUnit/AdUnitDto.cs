using Ookbee.Ads.Application.Business.AdGroup;
using Ookbee.Ads.Application.Business.AdUnitType;
using Ookbee.Ads.Application.Business.Publisher;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.AdUnit
{
    public class AdUnitDto : DefaultDto
    {
        public AdGroupDto AdGroup { get; set; }
        public string AdNetwork { get; set; }
        public string AdNetworkUnitId { get; set; }
        public int? SortSeq { get; set; }

        public static Expression<Func<AdUnitEntity, AdUnitDto>> Projection
        {
            get
            {
                return entity => new AdUnitDto()
                {
                    Id = entity.Id,
                    AdNetwork = entity.AdNetwork,
                    AdNetworkUnitId = entity.AdNetworkUnitId,
                    SortSeq = entity.SortSeq,
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
                    }
                };
            }
        }
    }
}