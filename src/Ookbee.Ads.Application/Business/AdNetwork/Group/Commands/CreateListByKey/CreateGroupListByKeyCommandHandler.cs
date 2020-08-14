using AutoMapper;
using MediatR;
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
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Group.Commands.CreateGroupListByKey
{
    public class CreateGroupListByKeyCommandHandler : IRequestHandler<CreateGroupListByKeyCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }
        private IDatabase AdsRedis { get; }

        public CreateGroupListByKeyCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<bool>> Handle(CreateGroupListByKeyCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(0, 100, request.AdGroupId));
            if (!getAdUnitList.Ok)
                return result.Fail(getAdUnitList.StatusCode, getAdUnitList.Message);

            if (getAdUnitList.Data.HasValue())
            {
                var data = Mapper.Map<List<GroupUnitDto>>(getAdUnitList.Data);
                var redisKey = CacheKey.GroupList(request.AdGroupId);
                var redisValue = JsonHelper.Serialize(data);
                var isSuccess = AdsRedis.StringSet(redisKey, redisValue);
                return result.Success(isSuccess);
            }

            return result.Success(true);
        }
    }
}
