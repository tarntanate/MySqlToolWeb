using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheAdById;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheAdIdListByPlatform;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheAdByUnitId
{
    public class DeleteCacheAdByUnitIdCommandHandler : IRequestHandler<DeleteCacheAdByUnitIdCommand, Unit>
    {
        private IMediator Mediator { get; }

        public DeleteCacheAdByUnitIdCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteCacheAdByUnitIdCommand request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteCacheAdIdListByPlatformCommand(request.AdUnitId));
            return Unit.Value;
        }
    }
}
