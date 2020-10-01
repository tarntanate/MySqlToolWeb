using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupRedis.Commands.UpdateAdGroupStatsRedis
{
    public class UpdateAdGroupStatsRedisCommand : IRequest<Response<bool>>
    {
        public long AdGroupId { get; private set; }
        public AdStatsType StatsType { get; private set; }
        public long Value { get; private set; }

        public UpdateAdGroupStatsRedisCommand(long adGroupId, AdStatsType statsType, long value = 1)
        {
            AdGroupId = adGroupId;
            StatsType = statsType;
            Value = value;
        }
    }
}
