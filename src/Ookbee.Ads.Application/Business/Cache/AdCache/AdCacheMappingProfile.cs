using AutoMapper;
using Ookbee.Ads.Application.Business.Advertisement.Ad;
using Ookbee.Ads.Application.Business.Advertisement.AdAsset;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ookbee.Ads.Application.Business.Cache.AdCache
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdDto, AdCacheDto>()
                .ForMember(dest => dest.CountdownSecond, m => m.MapFrom(src => src.CountdownSecond))
                .ForMember(dest => dest.ForegroundColor, m => m.MapFrom(src => src.ForegroundColor))
                .ForMember(dest => dest.BackgroundColor, m => m.MapFrom(src => src.BackgroundColor))
                .ForMember(dest => dest.LinkUrl, m => m.MapFrom(src => src.LinkUrl))
                .ForMember(dest => dest.UnitType, m => m.MapFrom(src => src.AdUnit.AdGroup.AdUnitType.Name))
                .ForMember(dest => dest.Assets, m => m.MapFrom(src => AssetsConverter(src.Assets)))
                .ForMember(dest => dest.Analytics, m => m.MapFrom(src => AnalyticsConverter(src.Analytics, src.AdUnit.Id, src.Id, src.Campaign.Id)));
        }

        private IEnumerable<AssetCacheDto> AssetsConverter(IEnumerable<AdAssetDto> assets)
        {
            return assets.Select(asset => new AssetCacheDto()
            {
                Position = asset.Position,
                AssetType = asset.AssetType,
                AssetPath = asset.AssetPath,
            });
        }

        private AnalyticsCacheDto AnalyticsConverter(IEnumerable<string> impressions, long adUnitId, long adId, long campaignId)
        {
            var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
            var analytics = new AnalyticsCacheDto()
            {
                Clicks = new List<string>() { $"{baseUrl}/api/ads/{adId}/stats?type={StatsType.Click}&campaignId={campaignId}".ToLower() },
                Impressions = new List<string>() { $"{baseUrl}/api/ads/{adId}/stats?type={StatsType.Impression}&campaignId={campaignId}".ToLower() }
            };

            if (impressions.HasValue())
                analytics.Impressions.AddRange(impressions);

            return analytics;
        }
    }
}
