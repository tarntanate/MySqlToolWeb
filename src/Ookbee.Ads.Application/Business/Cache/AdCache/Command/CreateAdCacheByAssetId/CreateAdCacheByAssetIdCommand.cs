﻿using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.CeateAdCacheByAssetId
{
    public class CreateAdCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdAssetId { get; set; }

        public CreateAdCacheByAssetIdCommand(long adAssetId)
        {
            AdAssetId = adAssetId;
        }
    }
}
