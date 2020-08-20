using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdById
{
    public class DeleteAdByIdCommandHandler : IRequestHandler<DeleteAdByIdCommand, Unit>
    {
        private IDatabase AdsRedis { get; }

        public DeleteAdByIdCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(DeleteAdByIdCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.Ad(request.AdId);
            var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
            if (keyExists)
                await AdsRedis.KeyDeleteAsync(redisKey);
            return Unit.Value;
        }
    }
}
