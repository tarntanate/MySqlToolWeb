using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherList;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupAvailableRedis;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupByPublisherRedis
{
    public class CreateAdGroupByPublisherRedisCommandHandler : IRequestHandler<CreateAdGroupByPublisherRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdGroupByPublisherRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupByPublisherRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                var getPublisherList = await Mediator.Send(new GetPublisherListQuery(start, length), cancellationToken);
                if (getPublisherList.IsSuccess)
                {
                    var publishers = getPublisherList.Data;
                    foreach (var publisher in publishers)
                    {
                        var redisKey = CacheKey.GroupIdsPublisher();
                        var publisherName = $"{publisher.Name}-{publisher.CountryCode}".ToUpper();
                        await Mediator.Send(new CreateAdGroupAvailableRedisCommand(publisher.Id, publisherName), cancellationToken);
                    }
                    next = publishers.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
