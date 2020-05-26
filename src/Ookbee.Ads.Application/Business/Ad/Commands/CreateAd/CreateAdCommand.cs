using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();

        public string CampaignId { get; set; }

        public string AdSlotId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan? Cooldown { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public List<string> Analytics { get; set; }

        public string AppLink { get; set; }

        public string WebLink { get; set; }

        public PlatformModel Platform { get; set; }

        public bool EnabledFlag => true;

        public CreateAdCommand()
        {

        }

        public CreateAdCommand(CreateAdCommand request)
        {
            CampaignId = request.CampaignId;
            AdSlotId = request.AdSlotId;
            Name = request.Name;
            Description = request.Description;
            Cooldown = request.Cooldown;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            AppLink = request.AppLink;
            WebLink = request.WebLink;
            Analytics = request.Analytics;
            Platform = request.Platform;
        }
    }
}
