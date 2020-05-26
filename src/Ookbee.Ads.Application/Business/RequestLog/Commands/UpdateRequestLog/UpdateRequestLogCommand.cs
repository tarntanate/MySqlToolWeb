using System;
using System.Collections.Generic;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Commands.UpdateRequestLog
{
    public class UpdateRequestLogCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string CampaignId { get; set; }

        public string RequestLogSlotId { get; set; }

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

        public UpdateRequestLogCommand()
        {

        }

        public UpdateRequestLogCommand(string id, UpdateRequestLogCommand request)
        {
            Id = id;
            CampaignId = request.CampaignId;
            RequestLogSlotId = request.RequestLogSlotId;
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
