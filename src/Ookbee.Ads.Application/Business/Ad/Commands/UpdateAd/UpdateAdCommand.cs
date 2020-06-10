using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }
        public long AdUnitId { get; set; }
        public long CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CooldownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string[] Analytics { get; set; }
        public string[] Platforms { get; set; }
        public string AppLink { get; set; }
        public string WebLink { get; set; }

        public UpdateAdCommand()
        {

        }

        public UpdateAdCommand(long id, UpdateAdCommand request)
        {
            Id = id;
            AdUnitId = request.AdUnitId;
            CampaignId = request.CampaignId;
            Name = request.Name;
            Description = request.Description;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Analytics = request.Analytics;
            Platforms = request.Platforms;
            AppLink = request.AppLink;
            WebLink = request.WebLink;
        }
    }
}
