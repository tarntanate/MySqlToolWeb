using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.GetAdIdListRedis
{
    public class GetAdIdListRedisCommand : IRequest<Response<IEnumerable<long>>>
    {
        public long AdUnitId { get; set; }

        public GetAdIdListRedisCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
