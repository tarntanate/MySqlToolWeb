using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Ookbee.Ads.Application.Business.Publisher;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;

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