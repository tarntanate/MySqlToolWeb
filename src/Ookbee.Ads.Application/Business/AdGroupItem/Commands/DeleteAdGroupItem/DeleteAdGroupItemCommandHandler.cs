using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.DeleteAdGroupItem
{
    public class DeleteAdGroupItemCommandHandler : IRequestHandler<DeleteAdGroupItemCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }

        public DeleteAdGroupItemCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo)
        {
            Mediator = mediator;
            AdGroupItemDbRepo = adGroupItemDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdGroupItemCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdGroupItemCommand request)
        {
            var result = new HttpResult<bool>();

            await AdGroupItemDbRepo.DeleteAsync(request.Id);
            await AdGroupItemDbRepo.SaveChangesAsync();

            return result.Success(true, request.Id, null);
        }
    }
}
