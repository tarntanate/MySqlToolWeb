﻿namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitRequest
    {
        public long AdGroupId { get; set; }
        public string AdNetwork { get; set; }
        public int? SortSeq { get; set; }
    }
}