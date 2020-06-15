using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.AdAsset
{
    public class AdAssetDto : DefaultDto
    {
        public long AdId { get; set; }
        public string AssetPath { get; set; }
        public string AssetType { get; set; }
        public string Position { get; set; }

        public static Expression<Func<AdAssetEntity, AdAssetDto>> Projection
        {
            get
            {
                return entity => new AdAssetDto()
                {
                    Id = entity.Id,
                    AdId = entity.AdId,
                    AssetType = entity.AssetType,
                    AssetPath = entity.AssetPath,
                    Position = entity.Position,
                };
            }
        }
    }
}