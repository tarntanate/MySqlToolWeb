using System;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Cooldown { get; set; }

        public string Position { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }
    }
}