﻿using MediatR;

namespace Ookbee.Ads.Application.Business.AdGroupCache.Commands.CreateAdGroupCache
{
    public class CreateAdGroupCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public CreateAdGroupCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
