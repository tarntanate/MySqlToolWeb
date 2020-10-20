using System.Collections.Generic;
using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherRedis
{
    public class GetAdGroupIdListByPublisherRedisQuery : IRequest<Response<Dictionary<string, ApiListResult<AdGroupEnabledCacheDto>>>>
    {
        public GetAdGroupIdListByPublisherRedisQuery()
        {
            
        }
    }
}
