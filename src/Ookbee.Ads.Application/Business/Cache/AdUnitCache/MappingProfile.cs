using AutoMapper;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit;
using Ookbee.Ads.Infrastructure;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdUnitDto, AdUnitCacheDto>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => AdNetworkUnitIdConverter(src)))
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.AdNetwork))
                .ForMember(dest => dest.Analytics, m => m.MapFrom(src => AnalyticsConverter(src)));
        }

        private string AdNetworkUnitIdConverter(AdUnitDto adUnit)
        {
            if (string.Equals("OOKBEE", adUnit.AdNetwork, StringComparison.OrdinalIgnoreCase))
            {
                return adUnit.Id.ToString();
            }
            return adUnit.AdNetworkUnitId;
        }

        private AnalyticsCacheDto AnalyticsConverter(AdUnitDto adUnit)
        {
            if (string.Equals("OOKBEE", adUnit.AdNetwork, StringComparison.OrdinalIgnoreCase))
            {
                var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var analytics = new AnalyticsCacheDto()
                {
                    Clicks = new List<string>() { $"{baseUrl}/api/units/{adUnit.Id}/stats?event=impression" },
                    Impressions = new List<string>() { $"{baseUrl}/api/units/{adUnit.Id}/stats?event=click" },
                };
                return analytics;
            }
            return null;
        }
    }
}
