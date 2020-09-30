using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListRedis
{
    public class GetAdGroupIdListRedisCommand : IRequest<Response<IEnumerable<long>>>
    {
        public GetAdGroupIdListRedisCommand()
        {

        }
    }
}
