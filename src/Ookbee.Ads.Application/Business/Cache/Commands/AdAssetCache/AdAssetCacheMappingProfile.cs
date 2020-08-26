using AutoMapper;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.AdAsset;
using Ookbee.Ads.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache
{
    public class AdAssetCacheMappingProfile : Profile
    {
        public AdAssetCacheMappingProfile()
        {
            CreateMap<AdDto, AdCacheDto>()
                .ForMember(dest => dest.CountdownSecond, m => m.MapFrom(src => src.CountdownSecond))
                .ForMember(dest => dest.ForegroundColor, m => m.MapFrom(src => src.ForegroundColor))
                .ForMember(dest => dest.BackgroundColor, m => m.MapFrom(src => src.BackgroundColor))
                .ForMember(dest => dest.LinkUrl, m => m.MapFrom(src => src.LinkUrl))
                .ForMember(dest => dest.UnitType, m => m.MapFrom(src => src.AdUnit.AdGroup.AdUnitType.Name))
                .ForMember(dest => dest.Assets, m => m.MapFrom(src => AssetsConverter(src.Assets)))
                .ForMember(dest => dest.Analytics, m => m.MapFrom(src => AnalyticsConverter(src.Analytics, src.AdUnit.Id, src.Id)));
        }

        private IEnumerable<AdAssetCacheDto> AssetsConverter(IEnumerable<AdAssetDto> assets)
        {
            return assets.Select(asset => new AdAssetCacheDto()
            {
                Position = asset.Position,
                AssetType = asset.AssetType,
                AssetPath = asset.AssetPath,
            });
        }

        private AdAnalyticsCacheDto AnalyticsConverter(IEnumerable<string> analytics, long adUnitId, long adId)
        {
            var analyticsBaseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
            var analyticsCache = new AdAnalyticsCacheDto()
            {
                Clicks = new List<string>() { $"{analyticsBaseUrl}/api/units/{adUnitId}/ads/{adId}/stats?event=click" },
                Impressions = new List<string>() { $"{analyticsBaseUrl}/api/units/{adUnitId}/ads/{adId}/stats?event=impression" },
            };
            return analyticsCache;
        }
    }
}
