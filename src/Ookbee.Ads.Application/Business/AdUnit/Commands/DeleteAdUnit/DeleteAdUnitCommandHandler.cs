using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheAdByUnitId;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommandHandler : IRequestHandler<DeleteAdUnitCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public DeleteAdUnitCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdUnitCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdUnitCommand request)
        {
            var result = new HttpResult<bool>();

            await AdUnitDbRepo.DeleteAsync(request.Id);
            await AdUnitDbRepo.SaveChangesAsync();

            await Mediator.Send(new DeleteCacheAdByUnitIdCommand(request.Id));

            return result.Success(true, request.Id, null);
        }
    }
}
