using System;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdCommand : IRequest<HttpResult<bool>>
    {
        public string CampaignId { get; set; }

        public string AdSlotId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Cooldown { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public bool EnabledFlag { get; set; }

        public UpdateAdCommand()
        {
            
        }

        public UpdateAdCommand(string id, UpdateAdCommand request)
        {
            CampaignId =request.CampaignId;
            AdSlotId = request.AdSlotId;
            Name = request.Name;
            Description = request.Description;
            Cooldown = request.Cooldown;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
        }
    }
}
