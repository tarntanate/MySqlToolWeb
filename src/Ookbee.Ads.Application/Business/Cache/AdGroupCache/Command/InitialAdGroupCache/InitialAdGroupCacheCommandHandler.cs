using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.CreateAdGroupCache;
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
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                if (getAdGroupList.Ok)
                {
                    foreach (var group in getAdGroupList.Data)
                    {
                        await Mediator.Send(new CreateAdGroupCacheCommand(group.Id));
                    }
                }
                next = getAdGroupList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            return Unit.Value;
        }
    }
}
