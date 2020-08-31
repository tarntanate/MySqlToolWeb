using System.Collections.Generic;
using AutoMapper;
using Ookbee.Ads.Application.Business.AdUnit;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache
{
    public class CreateAdUnitCacheMappingProfile : Profile
    {
        public CreateAdUnitCacheMappingProfile()
        {
            CreateMap<AdUnitDto, AdUnitCacheDto>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.AdNetwork.ToLower() == "ookbee" ? src.Id.ToString() : src.AdNetworkUnitId))
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.AdNetwork))
                .ForMember(dest => dest.Analytics, m => m.MapFrom(src => src.AdNetwork.ToLower() == "ookbee" ? null : AnalyticsConverter(src.Id)));
        }

        private AdAnalyticsCacheDto AnalyticsConverter(long adUnitId)
        {
            var analyticsBaseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;

            var impressions = new List<string>() { $"{analyticsBaseUrl}/api/units/{adUnitId}/stats?event=impression" };
            var clicks = new List<string>() { $"{analyticsBaseUrl}/api/units/{adUnitId}/stats?event=click" };
        
            var analyticsCache = new AdAnalyticsCacheDto()
            {
                Clicks = clicks,
                Impressions = impressions,
            };
            return analyticsCache;
        }
    }
}
