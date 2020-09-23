using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.CreateAdCache;
using Ookbee.Ads.Common.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.InitialAdCache
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
                next = false;
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, request.AdUnitId, null), cancellationToken);
                if (getAdList.Ok &&
                    getAdList.Data.HasValue())
                {
                    var items = getAdList.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new CreateAdCacheCommand(item.Id), cancellationToken);
                    }
                    start += length;
                    next = getAdList.Data.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
