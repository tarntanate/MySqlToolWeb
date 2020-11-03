using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdUnitIdListRedis
{
    public class GetAdUnitIdListRedisQuery : IRequest<Response<IEnumerable<long>>>
    {
        public long AdGroupId { get; set; }

        public GetAdUnitIdListRedisQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
