using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsCommand : IRequest<HttpResult<long>>
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
