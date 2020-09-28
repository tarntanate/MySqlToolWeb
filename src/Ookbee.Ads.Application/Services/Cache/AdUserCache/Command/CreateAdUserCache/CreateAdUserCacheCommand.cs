using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdUserCache.Commands.CreateAdUserCache
{
    public class CreateAdUserCacheCommand : IRequest<Unit>
    {
        public long UserId { get; set; }

        public CreateAdUserCacheCommand(long accountId)
        {
            UserId = accountId;
        }
    }
}
