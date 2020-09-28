using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.CreateAd
{
    public class CreateAdCommand : IRequest<Response<long>>
    {
        public long AdUnitId { get; set; }
        public long CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AdStatusType Status { get; set; }
        public int Quota { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public int? CooldownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public IEnumerable<string> Analytics { get; set; }
        public IEnumerable<AdPlatform> Platforms { get; set; }
        public string AppLink { get; set; }
        public string LinkUrl { get; set; }

        public CreateAdCommand(CreateAdRequest request)
        {
            AdUnitId = request.AdUnitId;
            CampaignId = request.CampaignId;
            Name = request.Name;
            Description = request.Description;
            Status = request.Status;
            Quota = request.Quota;
            StartAt = request.StartAt;
            EndAt = request.EndAt;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Analytics = request.Analytics;
            Platforms = request.Platforms;
            AppLink = request.AppLink;
            LinkUrl = request.LinkUrl;
        }
    }
}
