using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Advertiser;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit
{
    public class AdUnitNetworkUnitIdDto : DefaultDto
    {
        public AdPlatform Platform { get; set; }
        public string AdNetworkUnitId { get; set; }
    }
}