using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdUserCache.Commands.CreateAdUserCache;
using Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserListByRoleId;
using Ookbee.Ads.Common.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUserCache.Commands.InitialAdUserCache
{
    public class InitialAdUserCacheCommandHandler : IRequestHandler<InitialAdUserCacheCommand>
    {
        private readonly IMediator Mediator;

        public InitialAdUserCacheCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdUserCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                next = false;
                var getUserListByRoleIdRequest = new GetUserListByRoleIdQuery(start, length, 3);
                var getUserListByRoleIdResponse = await Mediator.Send(getUserListByRoleIdRequest, cancellationToken);
                if (getUserListByRoleIdResponse.Data.HasValue())
                {
                    var items = getUserListByRoleIdResponse.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new CreateAdUserCacheCommand(item), cancellationToken);
                    }
                    start += length;
                    next = getUserListByRoleIdResponse.Data.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
