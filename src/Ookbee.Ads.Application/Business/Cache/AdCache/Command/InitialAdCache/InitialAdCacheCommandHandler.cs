using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.CreateAdCache;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.InitialAdCache
{
    public class InitialAdCacheCommandHandler : IRequestHandler<InitialAdCacheCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdCacheCommandHandler(
            IMediator mediator)
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
                var adList = await Mediator.Send(new GetAdListQuery(start, length, request.AdUnitId, null), cancellationToken);
                if (adList.Ok)
                {
                    var ads = adList.Data;
                    foreach (var ad in ads)
                    {
                        await Mediator.Send(new CreateAdCacheCommand(ad.Id), cancellationToken);
                    }
                }
                next = adList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            return Unit.Value;
        }
    }
}
