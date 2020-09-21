using MediatR;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.CreateAdUnitStats
{
    public class CreateAdUnitStatsCommand : IRequest<HttpResult<long>>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdUnitId { get; set; }
        public long Request { get; set; }
        public long Fill { get; set; }

        public CreateAdUnitStatsCommand(DateTimeOffset caculatedAt, long adUnitId, long request, long fill)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
            Request = request;
            Fill = fill;
        }
    }
}
