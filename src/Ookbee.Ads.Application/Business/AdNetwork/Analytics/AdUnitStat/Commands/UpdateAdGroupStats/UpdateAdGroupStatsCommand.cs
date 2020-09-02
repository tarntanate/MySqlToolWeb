using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdUnitStatsCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public long AdUnitId { get; set; }
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }

        public UpdateAdUnitStatsCommand(long id, long adUnitId, DateTime caculatedAt, Platform platform, long request)
        {
            Id = id;
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
            Platform = platform;
            Request = request;
        }
    }
}
