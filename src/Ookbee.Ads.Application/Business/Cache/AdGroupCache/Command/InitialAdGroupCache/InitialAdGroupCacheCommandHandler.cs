using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.CreateAdGroupCache;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.InitialAdUnitCache;
using Ookbee.Ads.Common.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.InitialAdGroupCache
{
    public class InitialAdGroupCacheCommandHandler : IRequestHandler<InitialAdGroupCacheCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdGroupCacheCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                next = false;
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                if (getAdGroupList.Ok &&
                    getAdGroupList.Data.HasValue())
                {
                    var items = getAdGroupList.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new CreateAdGroupCacheCommand(item.Id));
                        await Mediator.Send(new InitialAdUnitCacheCommand(item.Id));
                    }
                    start += length;
                    next = items.Count() < length ? false : true;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
