using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsCommand : IRequest<Response<long>>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdGroupId { get; set; }
        public long Request { get; set; }

        public CreateAdGroupStatsCommand(DateTimeOffset caculatedAt, long adGroupId, long request)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
            Request = request;
        }
    }
}
