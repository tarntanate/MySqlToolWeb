using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdUnitListByGroupId
{
    public class CreateAdUnitListByGroupIdCommandHandler : IRequestHandler<CreateAdUnitListByGroupIdCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }
        private AdsRedisContext AdsRedis { get; }

        public CreateAdUnitListByGroupIdCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
            AdsRedis = adsRedis;
        }

        public async Task<HttpResult<bool>> Handle(CreateAdUnitListByGroupIdCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var start = 0;
            var length = 100;
            var next = true;
            var units = new List<AdNetworkGroupUnitDto>();
            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId));
                if (getAdUnitList.Ok)
                {
                    foreach (var adUnit in getAdUnitList.Data)
                    {
                        var item = Mapper.Map<AdNetworkGroupUnitDto>(adUnit);
                        units.Add(item);
                    }
                }
                next = getAdUnitList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            if (units.HasValue())
            {
                var redisKey = CacheKey.UnitByGroup(request.AdGroupId);
                var redisValue = JsonHelper.Serialize(units);
                if (await AdsRedis.Database(0).KeyExistsAsync(redisKey))
                    await AdsRedis.Database(0).KeyDeleteAsync(redisKey);
                await AdsRedis.Database(0).StringSetAsync(redisKey, redisValue);
            }

            return result.Success(true);
        }
    }
}
