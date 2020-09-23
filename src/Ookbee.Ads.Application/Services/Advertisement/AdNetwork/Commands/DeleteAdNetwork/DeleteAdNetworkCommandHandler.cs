using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.DeleteAdNetwork
{
    public class DeleteAdNetworkCommandHandler : IRequestHandler<DeleteAdNetworkCommand, Response<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo { get; }

        public DeleteAdNetworkCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            Mediator = mediator;
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdNetworkCommand request, CancellationToken cancellationToken)
        {
            await AdNetworkDbRepo.DeleteAsync(request.Id);
            await AdNetworkDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
