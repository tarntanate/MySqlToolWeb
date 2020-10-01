using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupStatsRedis
{
    public class CreateAdGroupStatsRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }
        public long AdGroupId { get; private set; }

        public CreateAdGroupStatsRedisCommand(DateTimeOffset caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
