using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerDto : DefaultDto
    {
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CountdownSecond { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ForegroundColor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BackgroundColor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AppLink { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LinkUrl { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BannerAnalyticsDto Analytics { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<BannerAssetDto> Assets { get; set; }

        public static Expression<Func<AdEntity, BannerDto>> Projection
        {
            get
            {
                return entity => new BannerDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CountdownSecond = entity.CountdownSecond,
                    ForegroundColor = entity.ForegroundColor,
                    BackgroundColor = entity.BackgroundColor,
                    AppLink = entity.AppLink,
                    LinkUrl = entity.WebLink,
                    Analytics = new BannerAnalyticsDto()
                    {
                        Clicks = null,
                        Impressions = entity.Analytics
                    },
                    Assets = entity.AdAssets
                        .Where(a => a.DeletedAt == null)
                        .Select(e => new BannerAssetDto()
                        {
                            Id = e.Id,
                            AssetPath = e.AssetPath,
                            AssetType = e.AssetType,
                            Position = e.Position,
                        })
                };
            }
        }

        public void AddClickUrl(string url)
        {
            if (!Analytics.Clicks.HasValue())
                Analytics.Clicks = new List<string>();
            Analytics.Clicks.Insert(0, url);
        }

        public void AddImpressionUrl(string url)
        {
            if (!Analytics.Impressions.HasValue())
                Analytics.Impressions = new List<string>();
            Analytics.Impressions.Insert(0, url);
        }
    }
}