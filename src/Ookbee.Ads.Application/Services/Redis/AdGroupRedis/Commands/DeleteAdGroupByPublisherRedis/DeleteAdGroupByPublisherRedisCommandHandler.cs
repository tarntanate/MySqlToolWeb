using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherByName;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherRedis;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.DeleteAdGroupByPublisherRedis
{
    public class DeleteAdGroupByPublisherRedisCommandHandler : IRequestHandler<DeleteAdGroupByPublisherRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public DeleteAdGroupByPublisherRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupByPublisherRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupIdListByPublisher = await Mediator.Send(new GetAdGroupIdListByPublisherRedisQuery(), cancellationToken);
            if (getAdGroupIdListByPublisher.IsSuccess)
            {
                var publishers = getAdGroupIdListByPublisher.Data;
                if (publishers.HasValue())
                {
                    foreach (var publisher in publishers)
                    {
                        var publisherName = publisher.Key.Split("-")[0];
                        var publisherCountryCode = publisher.Key.Split("-")[1];
                        var isExistsAdGroupById = await Mediator.Send(new IsExistsPublisherByNameQuery(publisherName, publisherCountryCode));
                        if (isExistsAdGroupById.IsFail)
                        {
                            var redisKey = RedisKeys.GroupIdsPublisher();
                            var hashField = publisher.Key;
                            await AdsRedis.HashDeleteAsync(redisKey, hashField, CommandFlags.FireAndForget);
                        }
                    }
                }
            }

            return Unit.Value;
        }
    }
}
