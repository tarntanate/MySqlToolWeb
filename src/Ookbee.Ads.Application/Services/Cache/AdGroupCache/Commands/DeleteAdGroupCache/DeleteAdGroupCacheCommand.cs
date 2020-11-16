using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.DeleteAdGroupCache
{
    public class DeleteAdGroupCacheCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }

        public DeleteAdGroupCacheCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
