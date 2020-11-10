using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit
{
    public class AdUnitNetworkDto
    {
        public string Name { get; set; }
        public IEnumerable<AdUnitNetworkUnitIdDto> AdNetworkUnits { get; set; }
    }
}