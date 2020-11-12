using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdUserRedis.Commands.GetAdUserPreviewListRedis
{
    public class GetAdUserPreviewListRedisQuery : IRequest<Response<IEnumerable<long>>>
    {

        public GetAdUserPreviewListRedisQuery()
        {

        }
    }
}
