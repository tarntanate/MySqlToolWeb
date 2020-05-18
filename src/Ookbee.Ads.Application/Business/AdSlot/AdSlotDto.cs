using Ookbee.Ads.Application.Infrastructure;

namespace Ookbee.Ads.Application.Business.AdSlot
{
    public class AdSlotDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DefaultDto Publisher { get; set; }

        public DefaultDto SlotType { get; set; }
    }
}