using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();

        public string AdSlotId { get; set; }

        public string CampaignId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Cooldown { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public bool EnabledFlag => true;

        public CreateAdCommand()
        {

        }

        public CreateAdCommand(CreateAdCommand request)
        {
            AdSlotId = request.AdSlotId;
            CampaignId = request.CampaignId;
            Name = request.Name;
            Description = request.Description;
            Cooldown = request.Cooldown;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
        }
    }
}
