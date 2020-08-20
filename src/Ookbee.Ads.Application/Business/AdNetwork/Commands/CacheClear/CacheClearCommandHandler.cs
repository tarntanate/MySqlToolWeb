using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CacheClear
{
    public class CacheClearCommandHandler : IRequestHandler<CacheClearCommand, HttpResult<bool>>
    {
        private AdsRedisContext AdsRedis { get; }

        public CacheClearCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis;
        }

        public async Task<HttpResult<bool>> Handle(CacheClearCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();
            try
            {
                await AdsRedis.FlushDatabase();
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
