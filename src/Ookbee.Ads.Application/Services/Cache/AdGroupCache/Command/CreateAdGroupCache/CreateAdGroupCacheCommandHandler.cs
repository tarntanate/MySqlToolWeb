using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.InitialAdUnitCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.CreateAdGroupCache
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
            await Mediator.Send(new InitialAdUnitCacheCommand(request.AdGroupId), cancellationToken);

            return Unit.Value;
        }
    }
}
