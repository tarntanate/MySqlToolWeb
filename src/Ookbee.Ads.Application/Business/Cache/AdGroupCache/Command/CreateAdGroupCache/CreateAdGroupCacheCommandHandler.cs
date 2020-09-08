using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupById;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.CreateAdUnitCache;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.CreateAdGroupCache
{
    public class CreateAdGroupCacheCommandHandler : IRequestHandler<CreateAdGroupCacheCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdGroupCacheCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(CreateAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupById = await Mediator.Send(new GetAdGroupByIdQuery(request.AdGroupId), cancellationToken);
            if (getAdGroupById.Ok)
            {
                await Mediator.Send(new CreateAdUnitCacheCommand(request.AdGroupId), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
