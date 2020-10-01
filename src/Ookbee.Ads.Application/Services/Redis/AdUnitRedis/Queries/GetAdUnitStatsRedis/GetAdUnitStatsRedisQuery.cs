using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdUnitStatsRedis
{
    public class GetAdUnitStatsRedisQuery : IRequest<Response<Dictionary<AdStatsType, long>>>
    {
        public long AdUnitId { get; set; }

        public GetAdUnitStatsRedisQuery(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
