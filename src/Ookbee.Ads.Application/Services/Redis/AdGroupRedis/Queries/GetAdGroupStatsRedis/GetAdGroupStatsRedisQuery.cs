using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupStatsRedis
{
    public class GetAdGroupStatsRedisQuery : IRequest<Response<Dictionary<AdStatsType, long>>>
    {
        public long AdGroupId { get; set; }

        public GetAdGroupStatsRedisQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
