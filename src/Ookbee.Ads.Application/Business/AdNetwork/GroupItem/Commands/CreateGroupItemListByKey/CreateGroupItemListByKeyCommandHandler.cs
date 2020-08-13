using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemList;
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

namespace Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Commands.CreateGroupItemListByKey
{
    public class CreateGroupItemListByKeyCommandHandler : IRequestHandler<CreateGroupItemListByKeyCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }
        private IDatabase AdsRedis { get; }

        public CreateGroupItemListByKeyCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupItemDbRepo = adGroupItemDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<bool>> Handle(CreateGroupItemListByKeyCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var getAdGroupItemList = await Mediator.Send(new GetAdGroupItemListQuery(0, 100, request.AdGroupId));
            if (!getAdGroupItemList.Ok)
                return result.Fail(getAdGroupItemList.StatusCode, getAdGroupItemList.Message);

            if (getAdGroupItemList.Data.HasValue())
            {
                var data = Mapper.Map<List<GroupItemUnitDto>>(getAdGroupItemList.Data);
                var redisKey = CacheKey.GroupItemList(request.AdGroupId);
                var redisValue = JsonHelper.Serialize(data);
                var isSuccess = AdsRedis.StringSet(redisKey, redisValue);
                return result.Success(isSuccess);
            }

            return result.Success(true);
        }
    }
}
