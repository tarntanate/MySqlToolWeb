using MediatR;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.Commands.CacheClear
{
    public class CacheClearCommandHandler : IRequestHandler<CacheClearCommand, Unit>
    {
        private AdsRedisContext AdsRedis { get; }

        public CacheClearCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis;
        }

        public async Task<Unit> Handle(CacheClearCommand request, CancellationToken cancellationToken)
        {
            await AdsRedis.FlushDatabase();

            return Unit.Value;
        }
    }
}
