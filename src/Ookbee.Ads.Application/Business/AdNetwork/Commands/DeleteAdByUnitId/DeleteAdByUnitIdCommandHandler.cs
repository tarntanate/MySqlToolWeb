using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdIdListByPlatform;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdByUnitId
{
    public class DeleteAdByUnitIdCommandHandler : IRequestHandler<DeleteAdByUnitIdCommand, Unit>
    {
        private IMediator Mediator { get; }

        public DeleteAdByUnitIdCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteAdByUnitIdCommand request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteAdIdListByPlatformCommand(request.AdUnitId));
            return Unit.Value;
        }
    }
}
