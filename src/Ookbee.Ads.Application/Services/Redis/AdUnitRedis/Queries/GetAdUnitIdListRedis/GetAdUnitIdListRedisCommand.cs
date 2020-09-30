using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdUnitIdListRedis
{
    public class GetAdUnitIdListRedisCommand : IRequest<Response<IEnumerable<long>>>
    {
        public long AdUnitId { get; set; }

        public GetAdUnitIdListRedisCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
