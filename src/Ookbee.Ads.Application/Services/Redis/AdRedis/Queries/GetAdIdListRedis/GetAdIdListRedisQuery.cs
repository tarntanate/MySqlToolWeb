using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.GetAdIdListRedis
{
    public class GetAdIdListRedisQuery : IRequest<Response<IEnumerable<long>>>
    {
        public long AdUnitId { get; set; }

        public GetAdIdListRedisQuery(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
