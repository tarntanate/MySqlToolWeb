using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Identity.User.Queries.IsExistsUserById;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUserCache.Commands.CreateAdUserCache
{
    public class CreateAdCacheCommandHandler : IRequestHandler<CreateAdUserCacheCommand>
    {
        private readonly IMediator Mediator;
        private IDatabase AdsRedis { get; }

        public CreateAdCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUserCacheCommand request, CancellationToken cancellationToken)
        {
            var isExistsUserById = await Mediator.Send(new IsExistsUserByIdQuery(request.UserId), cancellationToken);
            if (isExistsUserById.Ok &&
                isExistsUserById.Data.HasValue())
            {
                var redisKey = CacheKey.UserPreview();
                var redisValue = request.UserId;
                await AdsRedis.SetAddAsync(redisKey, redisValue);
            }

            return Unit.Value;
        }
    }
}
