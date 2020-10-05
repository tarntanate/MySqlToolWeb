﻿using MediatR;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitIdRedis
{
    public class CreateAdUnitIdRedisCommand : IRequest<Unit>
    {
        public long AdUnitId { get; private set; }

        public CreateAdUnitIdRedisCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}