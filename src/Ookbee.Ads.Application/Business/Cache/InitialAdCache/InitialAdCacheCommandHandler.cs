using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.CreateAdGroupCache;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.Commands.InitialAdCache
{
    public class InitialAdCacheCommandHandler : IRequestHandler<InitialAdCacheCommand, Unit>
    {
        private IMediator Mediator { get; }

        public InitialAdCacheCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialAdCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                if (getAdGroupList.Ok)
                {
                    foreach (var adGroup in getAdGroupList.Data)
                    {
                        await Mediator.Send(new CreateAdGroupCacheCommand(adGroup.Id), cancellationToken);
                    }
                    start += length;
                }
                next = getAdGroupList.Data.Count() < length ? false : true;
            }
            while (next);

            return Unit.Value;
        }
    }
}
