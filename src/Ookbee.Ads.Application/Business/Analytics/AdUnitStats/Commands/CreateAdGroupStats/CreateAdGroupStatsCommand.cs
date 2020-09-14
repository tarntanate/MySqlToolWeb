using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.CreateAdUnitStats
{
    public class CreateAdUnitStatsCommand : IRequest<HttpResult<long>>
    {
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public long AdUnitId { get; set; }
        public long Request { get; set; }
        public long Fill { get; set; }

        public CreateAdUnitStatsCommand(DateTime caculatedAt, Platform platform, long adUnitId, long request, long fill)
        {
            CaculatedAt = caculatedAt;
            Platform = platform;
            AdUnitId = adUnitId;
            Request = request;
            Fill = fill;
        }
    }
}
