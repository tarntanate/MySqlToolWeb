﻿using MediatR;

namespace Ookbee.Ads.Application.Business.AdUnitCache.Commands.UpdateAdUnitCache
{
    public class UpdateAdUnitCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public UpdateAdUnitCacheCommand(long adGroupId)
        {
            AdUnitId = adGroupId;
        }
    }
}
