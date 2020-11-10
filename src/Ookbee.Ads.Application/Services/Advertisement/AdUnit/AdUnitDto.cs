using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType;
using Ookbee.Ads.Application.Services.Advertisement.Publisher;
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
                    AdGroup = new AdGroupDto()
                    {
                        Id = entity.AdGroup.Id,
                        Name = entity.AdGroup.Name,
                        Description = entity.AdGroup.Description,
                        AdGroupType = new AdGroupTypeDto()
                        {
                            Id = entity.AdGroup.AdGroupType.Id,
                            Name = entity.AdGroup.AdGroupType.Name,
                            Description = entity.AdGroup.AdGroupType.Description
                        },
                        Publisher = new PublisherDto()
                        {
                            Id = entity.AdGroup.Publisher.Id,
                            Name = entity.AdGroup.Publisher.Name,
                            Description = entity.AdGroup.Publisher.Description
                        }
                    },
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