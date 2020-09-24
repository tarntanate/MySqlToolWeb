using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAd
{
    public class UpdateAdCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public long AdUnitId { get; private set; }
        public long CampaignId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AdStatus Status { get; private set; }
        public int? Quota { get; private set; }
        public DateTimeOffset? StartAt { get; private set; }
        public DateTimeOffset? EndAt { get; private set; }
        public int? CooldownSecond { get; private set; }
        public string ForegroundColor { get; private set; }
        public string BackgroundColor { get; private set; }
        public IEnumerable<string> Analytics { get; private set; }
        public IEnumerable<Platform> Platforms { get; private set; }
        public string AppLink { get; private set; }
        public string LinkUrl { get; private set; }

        public UpdateAdCommand(long id, UpdateAdRequest request)
        {
            Id = id;
            AdUnitId = request.AdUnitId;
            CampaignId = request.CampaignId;
            Name = request.Name;
            Description = request.Description;
            Status = request.Status;
            Quota = request.Quota;
            StartAt = request.StartAt;
            EndAt = request.EndAt;
            CooldownSecond = request.CooldownSecond;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Analytics = request.Analytics;
            Platforms = request.Platforms;
            AppLink = request.AppLink;
            LinkUrl = request.LinkUrl;
        }
    }
}
