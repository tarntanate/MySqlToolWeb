using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork
{
    public class AdNetworkDto : DefaultDto
    {
        public AdPlatform Platform { get; set; }
        public long AdUnitId { get; set; }
        public string AdNetwork { get; set; }
        public string AdNetworkUnitId { get; set; }
    }
}
