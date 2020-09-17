using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.DeleteAdNetwork
{
    public class DeleteAdNetworkCommandHandler : IRequestHandler<DeleteAdNetworkCommand, HttpResult<bool>>
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

        public async Task<HttpResult<bool>> Handle(DeleteAdNetworkCommand request, CancellationToken cancellationToken)
        {
            await AdNetworkDbRepo.DeleteAsync(request.Id);
            await AdNetworkDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
