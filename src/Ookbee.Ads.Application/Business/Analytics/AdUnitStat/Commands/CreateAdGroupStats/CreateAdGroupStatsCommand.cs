using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.CreateAdUnitStats
{
    public class CreateAdUnitStatsCommand : IRequest<HttpResult<long>>
    {
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }
        public long Fill { get; set; }

        public CreateAdUnitStatsCommand(long adUnitId, DateTime caculatedAt, Platform platform, long request, long fill)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
            Platform = platform;
            Request = request;
            Fill = fill;
        }
    }
}
