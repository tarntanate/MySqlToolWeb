using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommandHandler : IRequestHandler<DeleteAdUnitTypeCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public DeleteAdUnitTypeCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            Mediator = mediator;
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdUnitTypeCommand request)
        {
            var result = new HttpResult<bool>();

            await AdUnitTypeDbRepo.DeleteAsync(request.Id);
            await AdUnitTypeDbRepo.SaveChangesAsync();

            return result.Success(true, request.Id, null);
        }
    }
}
