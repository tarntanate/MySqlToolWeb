using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommandHandler : IRequestHandler<DeleteAdUnitCommand, Response<bool>>
    {
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public DeleteAdUnitCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdUnitCommand request, CancellationToken cancellationToken)
        {
            await AdUnitDbRepo.DeleteAsync(request.Id);
            await AdUnitDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
