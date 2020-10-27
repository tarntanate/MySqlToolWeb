using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.DeleteAdGroupRedis
{
    public class DeleteAdGroupRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }

        public DeleteAdGroupRedisCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
