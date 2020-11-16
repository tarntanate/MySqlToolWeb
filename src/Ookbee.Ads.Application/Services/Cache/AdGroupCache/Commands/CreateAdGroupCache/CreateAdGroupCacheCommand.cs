using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.CreateAdGroupCache
{
    public class CreateAdGroupCacheCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }

        public CreateAdGroupCacheCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
