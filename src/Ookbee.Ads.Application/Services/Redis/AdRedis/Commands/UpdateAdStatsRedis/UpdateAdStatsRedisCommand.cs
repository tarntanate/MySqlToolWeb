using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.UpdateAdStatsRedis
{
    public class UpdateAdStatsRedisCommand : IRequest<Response<bool>>
    {
        public long AdId { get; set; }
        public AdStatsType StatsType { get; set; }
        public long Value { get; set; }

        public UpdateAdStatsRedisCommand(long adId, AdStatsType statsType, long value = 1)
        {
            AdId = adId;
            StatsType = statsType;
            Value = value;
        }
    }
}
