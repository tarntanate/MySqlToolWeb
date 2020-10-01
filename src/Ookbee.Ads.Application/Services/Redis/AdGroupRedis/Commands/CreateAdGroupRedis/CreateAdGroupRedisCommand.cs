using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupRedis
{
    public class CreateAdGroupRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }

        public CreateAdGroupRedisCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
