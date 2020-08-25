﻿using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.DeleteAdGroupCache
{
    public class DeleteAdGroupCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public DeleteAdGroupCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}