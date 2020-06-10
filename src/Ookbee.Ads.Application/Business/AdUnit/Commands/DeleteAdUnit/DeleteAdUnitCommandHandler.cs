using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommandHandler : IRequestHandler<DeleteAdUnitCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdUnitEntity> AdUnitEFCoreRepo { get; }

        public DeleteAdUnitCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdUnitEntity> adUnitEFCoreRepo)
        {
            Mediator = mediator;
            AdUnitEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdUnitCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdUnitCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await AdUnitEFCoreRepo.DeleteAsync(request.Id);
            await AdUnitEFCoreRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
