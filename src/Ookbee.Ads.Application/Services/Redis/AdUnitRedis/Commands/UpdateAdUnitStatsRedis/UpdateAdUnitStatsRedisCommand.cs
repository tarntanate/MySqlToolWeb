using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.UpdateAdUnitStatsRedis
{
    public class UpdateAdUnitStatsRedisCommand : IRequest<Response<bool>>
    {
        public long AdUnitId { get; set; }
        public AdStatsType StatsType { get; set; }
        public long Value { get; set; }

        public UpdateAdUnitStatsRedisCommand(long adUnitId, AdStatsType statsType, long value = 1)
        {
            AdUnitId = adUnitId;
            StatsType = statsType;
            Value = value;
        }
    }
}
