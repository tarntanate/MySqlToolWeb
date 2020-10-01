using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.IsExistsUserRoleMappingById;
using Ookbee.Ads.Application.Services.Redis.AdUserRedis.Commands.GetAdUserPreviewListRedis;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.DeleteAdUserPreviewRedis
{
    public class DeleteAdUserRedisPreviewCommandHandler : IRequestHandler<DeleteAdUserPreviewRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public DeleteAdUserRedisPreviewCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdUserPreviewRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdUserList = await Mediator.Send(new GetAdUserPreviewListRedisQuery(), cancellationToken);
            if (getAdUserList.IsSuccess)
            {
                var adUserIds = getAdUserList.Data;
                foreach (var adUserId in adUserIds)
                {
                    var isExistsUserRole = await Mediator.Send(new IsExistsUserRoleMappingByIdQuery(adUserId, 3), cancellationToken);
                    if (isExistsUserRole.IsFail)
                    {
                        var redisKey = CacheKey.UserIdsPreview();
                        var redisValue = adUserId;
                        await AdsRedis.SetRemoveAsync(redisKey, redisValue);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
