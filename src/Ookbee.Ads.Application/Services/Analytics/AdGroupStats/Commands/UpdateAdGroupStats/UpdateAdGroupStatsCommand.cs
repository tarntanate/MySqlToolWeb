using System;
using MediatR;
using Microsoft.DotNet.PlatformAbstractions;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Commands.UpdateAdGroupStats
{
    public class UpdateAdGroupStatsCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public UpdateAdGroupStatsCommand(long id, long adGroupId, Platform platform, long request, DateTimeOffset caculatedAt)
        {
            Id = id;
            AdGroupId = adGroupId;
            Platform = platform;
            Request = request;
            CaculatedAt = caculatedAt;
        }
    }
}
