using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.GetAdStatsRedis
{
    public class GetAdStatsRedisQuery : IRequest<Response<Dictionary<AdStatsType, long>>>
    {
        public long AdId { get; set; }

        public GetAdStatsRedisQuery(long adId)
        {
            AdId = adId;
        }
    }
}
