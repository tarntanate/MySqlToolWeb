﻿using AutoMapper;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache
{
    public class AdUnitCacheMappingProfile : Profile
    {
        public AdUnitCacheMappingProfile()
        {
            CreateMap<AdUnitDto, AdUnitCacheDto>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => AdNetworkUnitIdConverter(src)))
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.AdNetwork.Name))
                .ForMember(dest => dest.Analytics, m => m.MapFrom(src => AnalyticsConverter(src)));
        }

        private string AdNetworkUnitIdConverter(AdUnitDto adUnit)
        {
            var ookbee = AdNetwork.Ookbee.ToString();
            if (string.Equals(ookbee, adUnit.AdNetwork.Name, StringComparison.OrdinalIgnoreCase))
            {
                return adUnit.Id.ToString();
            }

            return adUnit?.AdNetwork?.AdNetworkUnits?.FirstOrDefault()?.AdNetworkUnitId;
        }

        private AnalyticsCacheDto AnalyticsConverter(AdUnitDto adUnit)
        {
            var ookbee = AdNetwork.Ookbee.ToString();
            if (!string.Equals(ookbee, adUnit.AdNetwork.Name, StringComparison.OrdinalIgnoreCase))
            {
                var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var analytics = new AnalyticsCacheDto()
                {
                    Clicks = new List<string>() { $"{baseUrl}/api/units/{adUnit.Id}/stats?type={AdStatsType.Click}".ToLower() },
                    Impressions = new List<string>() { $"{baseUrl}/api/units/{adUnit.Id}/stats?type={AdStatsType.Impression}".ToLower() },
                };
                return analytics;
            }
            return null;
        }
    }
}
