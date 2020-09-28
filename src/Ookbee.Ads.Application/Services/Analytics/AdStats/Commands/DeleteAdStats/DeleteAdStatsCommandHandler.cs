using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.DeleteAdStatsCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.DeleteAdStats
{
    public class DeleteAdStatsCommandHandler : IRequestHandler<DeleteAdStatsCommand>
    {
        private readonly IMediator Mediator;

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
