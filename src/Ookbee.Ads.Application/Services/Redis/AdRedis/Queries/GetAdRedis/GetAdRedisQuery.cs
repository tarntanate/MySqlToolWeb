﻿using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdRedis.Commands.GetAdRedis
{
    public class GetAdRedisQuery : IRequest<Response<string>>
    {
        public AdPlatform Platform { get; set; }
        public long AdUnitId { get; set; }
        public long? UserId { get; set; }

        public GetAdRedisQuery(string platform, long adUnitId, long? userId)
        {
            Platform = EnumHelper.ConvertTo<AdPlatform>(platform);
            AdUnitId = adUnitId;
            UserId = userId;
        }
    }
}
