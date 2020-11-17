using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupAvailableRedis
{
    public class CreateAdGroupAvailableRedisCommandHandler : IRequestHandler<CreateAdGroupAvailableRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdGroupAvailableRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupAvailableRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            var adGroups = new List<AdGroupEnabledCacheDto>();
            do
            {
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(start, length, null, request.PublisherId, null), cancellationToken);
                if (getAdGroupList.IsSuccess)
                {
                    var items = getAdGroupList.Data.Select(group => new AdGroupEnabledCacheDto
                    {
                        Id = group.Id,
                        Enabled = group.Enabled,
                        Placement = group.Placement,
                        Type = group.AdGroupType.Name
                    });
                    adGroups.AddRange(items);
                    next = adGroups.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            var cacheObj = new ApiListResult<AdGroupEnabledCacheDto>();
            cacheObj.Data.Items = adGroups;

            var hashField = request.PublisherName.ToUpper();
            var hashValue = JsonHelper.Serialize(cacheObj);
            var hashEntry = new HashEntry(hashField, hashValue);
            var redisKey = CacheKey.GroupIdsPublisher();
            await AdsRedis.HashSetAsync(redisKey, new HashEntry[] { hashEntry }, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
