﻿using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherList;
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

namespace Ookbee.Ads.Application.Business.AdNetworkGroup.Commands.CreateAdNetworkGroupListByKey
{
    public class CreateAdNetworkGroupListByKeyCommandHandler : IRequestHandler<CreateAdNetworkGroupListByKeyCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdNetworkGroupListByKeyCommandHandler(
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

        public async Task<HttpResult<bool>> Handle(CreateAdNetworkGroupListByKeyCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            await GenByGroup();

            return result.Success(true);
        }

        private async Task GenByGroup()
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(0, 100, null, null));
                if (getAdGroupList.Ok)
                {
                    foreach (var group in getAdGroupList.Data)
                    {
                        await GenByUnit(group.Id);
                    }
                }
                next = getAdGroupList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);
        }

        private async Task GenByUnit(long groupId)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(0, 100, groupId));
                if (getAdUnitList.Ok)
                {
                    foreach (var unit in getAdUnitList.Data)
                    {
                        var data = Mapper.Map<List<GroupUnitDto>>(getAdUnitList.Data);
                        var redisKey = CacheKey.UnitByGroup(groupId);
                        var redisValue = JsonHelper.Serialize(data);
                        var isSuccess = AdsRedis.StringSet(redisKey, redisValue);
                    }
                }
                next = getAdUnitList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);
        }
    }
}
