using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit
{
    public class AdUnitNetworkUnitIdDto : DefaultDto
    {
        public Platform Platform { get; set; }
        public string AdNetworkUnitId { get; set; }
    }
}