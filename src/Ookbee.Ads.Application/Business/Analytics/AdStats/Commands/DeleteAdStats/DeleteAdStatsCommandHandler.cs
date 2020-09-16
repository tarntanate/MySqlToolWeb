using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.DeleteAdStatsCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.DeleteAdStats
{
    public class DeleteAdStatsCommandHandler : IRequestHandler<DeleteAdStatsCommand>
    {
        private IMediator Mediator { get; }

        public DeleteAdStatsCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteAdStatsCommand request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteAdStatsCacheCommand(request.AdId), cancellationToken);

            return Unit.Value;
        }
    }
}
