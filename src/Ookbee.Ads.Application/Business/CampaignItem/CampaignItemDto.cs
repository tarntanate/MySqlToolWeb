using System;

namespace Ookbee.Ads.Application.Business.CampaignItem
{
    public class CampaignItemDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Cooldown { get; set; }

        public string Position { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }
    }
}