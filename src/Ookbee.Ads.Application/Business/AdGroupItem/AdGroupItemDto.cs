using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.AdGroupItem
{
    public class AdGroupItemDto : DefaultDto
    {
        public string AdUnitKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SortSeq { get; set; }

        public static Expression<Func<AdGroupItemEntity, AdGroupItemDto>> Projection
        {
            get
            {
                return entity => new AdGroupItemDto()
                {
                    Id = entity.Id,
                    AdUnitKey = entity.AdUnitKey,
                    Name = entity.Name,
                    Description = entity.Description,
                    SortSeq = entity.SortSeq,
                };
            }
        }
    }
}