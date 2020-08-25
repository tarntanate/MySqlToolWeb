﻿using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache
{
    public class AdCacheDto
    {
        public int? CountdownSecond { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public string LinkUrl { get; set; }

        public string UnitType { get; set; }

        public IEnumerable<AdAssetCacheDto> Assets { get; set; }

        public AdAnalyticsCacheDto Analytics { get; set; }

    }
}