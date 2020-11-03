﻿using MediatR;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitIdRedis
{
    public class CreateAdUnitIdRedisCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }

        public CreateAdUnitIdRedisCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
