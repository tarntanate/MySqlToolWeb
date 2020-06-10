using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommandHandler : IRequestHandler<DeleteAdUnitTypeCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdUnitTypeEntity> AdUnitTypeEFCoreRepo { get; }

        public DeleteAdUnitTypeCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdUnitTypeEntity> adUnitTypeEFCoreRepo)
        {
            Mediator = mediator;
            AdUnitTypeEFCoreRepo = adUnitTypeEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdUnitTypeCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsAdUnitTypeByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await AdUnitTypeEFCoreRepo.DeleteAsync(request.Id);
            await AdUnitTypeEFCoreRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
