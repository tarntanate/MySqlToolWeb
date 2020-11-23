using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserIdListByPermissionName;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUserRedis.Commands.CreateAdUserPreviewRedis
{
    public class CreateAdUserPreviewRedisCommandHandler : IRequestHandler<CreateAdUserPreviewRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdUserPreviewRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUserPreviewRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                var gerUserIdList = await Mediator.Send(new GetUserIdListByPermissionNameQuery(start, length, "Preview"), cancellationToken);
                if (gerUserIdList.IsSuccess)
                {
                    var userIds = gerUserIdList.Data;
                    foreach (var userId in userIds)
                    {
                        var redisKey = RedisKeys.UserIdsPreview();
                        var redisValue = userId;
                        var hashExists = await AdsRedis.SetAddAsync(redisKey, redisValue);
                    }
                    next = userIds.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
