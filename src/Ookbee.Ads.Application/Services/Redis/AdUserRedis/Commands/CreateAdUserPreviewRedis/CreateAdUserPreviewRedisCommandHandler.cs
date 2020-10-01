using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.GetUserRoleMappingList;
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
                var gerUserList = await Mediator.Send(new GetUserRoleMappingListQuery(start, length, null, 3), cancellationToken);
                if (gerUserList.IsSuccess)
                {
                    var users = gerUserList.Data;
                    foreach (var user in users)
                    {
                        var redisKey = CacheKey.UserIdsPreview();
                        var redisValue = user.Id;
                        var hashExists = await AdsRedis.SetAddAsync(redisKey, redisValue);
                    }
                    next = users.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
