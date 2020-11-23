using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherList;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupAvailableRedis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupByPublisherRedis
{
    public class CreateAdGroupByPublisherRedisCommandHandler : IRequestHandler<CreateAdGroupByPublisherRedisCommand>
    {
        private readonly IMediator Mediator;

        public CreateAdGroupByPublisherRedisCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
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
                        var redisKey = RedisKeys.GroupIdsPublisher();
                        var publisherName = $"{publisher.Name}-{publisher.CountryCode}".ToUpper();
                        await Mediator.Send(new CreateAdGroupAvailableRedisCommand(publisher.Id, publisherName), cancellationToken);
                    }
                    next = publishers.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
