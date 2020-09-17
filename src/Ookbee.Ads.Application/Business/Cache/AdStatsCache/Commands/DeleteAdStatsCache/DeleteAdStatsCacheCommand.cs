﻿using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.DeleteAdStatsCache
{
    public class DeleteAdStatsCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public DeleteAdStatsCacheCommand(long adId)
        {
            AdId = adId;
        }
    }
}