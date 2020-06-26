using Ookbee.Ads.Infrastructure.Enums;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitRequest
    {
        public long AdUnitTypeId { get; set; }
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AdNetwork AdNetworks { get; set; }
    }
}
