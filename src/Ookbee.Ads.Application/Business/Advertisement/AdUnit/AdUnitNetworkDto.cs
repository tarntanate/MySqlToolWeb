using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit
{
    public class AdNetworkDto
    {
        public string Name { get; set; }
        public IEnumerable<AdUnitNetworkUnitIdDto> UnitIds { get; set; }
    }
}