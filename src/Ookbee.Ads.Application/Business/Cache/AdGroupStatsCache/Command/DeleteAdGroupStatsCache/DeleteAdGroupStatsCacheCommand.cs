using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.DeleteAdGroupStatsCache
{
    public class DeleteAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public DeleteAdGroupStatsCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
