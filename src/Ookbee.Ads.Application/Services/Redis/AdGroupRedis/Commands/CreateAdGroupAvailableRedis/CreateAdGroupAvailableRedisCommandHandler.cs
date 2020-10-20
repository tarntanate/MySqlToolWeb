using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupIdList;
using Ookbee.Ads.Common.Helpers;
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
            var adGroupIds = new List<long>();
            do
            {
                var getAdGroupIdList = await Mediator.Send(new GetAdGroupIdListQuery(start, length, null, request.PublisherId, true), cancellationToken);
                if (getAdGroupIdList.IsSuccess)
                {
                    var items = getAdGroupIdList.Data;
                    adGroupIds.AddRange(items);
                    next = adGroupIds.Count() == length ? true : false;
                }
            }
            while (next);

            if (adGroupIds.Count() > 0)
            {
                var redisKey = CacheKey.GroupIdsPublisher();
                var redisValue = new HashEntry(request.PublisherName.ToUpper(), JsonHelper.Serialize(adGroupIds));
                await AdsRedis.HashSetAsync(redisKey, new HashEntry[] { redisValue }, CommandFlags.FireAndForget);
            }

            return Unit.Value;
        }
    }
}
