using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupAvailableRedis
{
    public class CreateAdGroupAvailableRedisCommand : IRequest<Unit>
    {
        public long PublisherId { get; private set; }
        public string PublisherName { get; private set; }

        public CreateAdGroupAvailableRedisCommand(long publisherId, string publisherName)
        {
            PublisherId = publisherId;
            PublisherName = publisherName;
        }
    }
}
