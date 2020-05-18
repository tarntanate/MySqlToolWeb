using System;
using Ookbee.Ads.Application.Business.AdSlot;
using Ookbee.Ads.Application.Business.Campaign;

namespace Ookbee.Ads.Application.Business.Ad
{
    public class AdDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Cooldown { get; set; }

        public string Position { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public CampaignDto Campaign { get; set; }

        public AdSlotDto AdSlot { get; set; }
    }
}