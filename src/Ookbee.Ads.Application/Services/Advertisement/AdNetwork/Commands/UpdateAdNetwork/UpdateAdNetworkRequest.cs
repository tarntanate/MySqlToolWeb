using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkRequest
    {
        public long AdUnitId { get; set; }
        public string AdNetworkUnitId { get; set; }
        public Platform Platform { get; set; }
    }
}
