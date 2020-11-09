using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit;
using Ookbee.Ads.Application.Services.Advertisement.Campaign;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad
{
    public class AdDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AdStatusType Status { get; set; }
        public int? Quota { get; set; }
        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? EndAt { get; set; }
        public int? CountdownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string LinkUrl { get; set; }
        public IEnumerable<string> Analytics { get; set; }
        public IEnumerable<AdPlatform> Platforms { get; set; }
        public IEnumerable<AdAssetDto> Assets { get; set; }
        public AdUnitDto AdUnit { get; set; }
        public CampaignDto Campaign { get; set; }

        public static AdDto FromEntity(AdEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<AdEntity, AdDto>> Projection
        {
            get
            {
                return entity => new AdDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    Status = entity.Status,
                    CountdownSecond = entity.CountdownSecond,
                    Quota = entity.Quota,
                    StartAt = entity.StartAt,
                    EndAt = entity.EndAt,
                    ForegroundColor = entity.ForegroundColor,
                    BackgroundColor = entity.BackgroundColor,
                    LinkUrl = entity.WebLink,
                    Analytics = entity.Analytics,
                    Platforms = entity.Platforms,
                    Assets = entity.AdAssets.Where(asset => asset.DeletedAt == null).AsQueryable().Select(AdAssetDto.Projection),
                    AdUnit = AdUnitDto.FromEntity(entity.AdUnit),
                    Campaign = CampaignDto.FromEntity(entity.Campaign)
                };
            }
        }
    }
}