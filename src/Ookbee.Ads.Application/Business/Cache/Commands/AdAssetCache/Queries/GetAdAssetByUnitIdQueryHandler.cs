using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.GetAdAssetByUnitId
{
    public class GetAdAssetByUnitIdQueryHandler : IRequestHandler<GetAdAssetByUnitIdQuery, HttpResult<string>>
    {
        private IMapper Mapper { get; }
        private IDatabase AdsRedis { get; }

        public GetAdAssetByUnitIdQueryHandler(
            IMapper mapper,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<HttpResult<string>> Handle(GetAdAssetByUnitIdQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();



            return result.Success(string.Empty);
        }
    }
}
