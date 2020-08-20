using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheUnitListByGroupId;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.DeleteAdGroup
{
    public class DeleteAdGroupCommandHandler : IRequestHandler<DeleteAdGroupCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public DeleteAdGroupCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdGroupCommand request)
        {
            var result = new HttpResult<bool>();

            await AdGroupDbRepo.DeleteAsync(request.Id);
            await AdGroupDbRepo.SaveChangesAsync();

            await Mediator.Send(new DeleteCacheUnitListByGroupIdCommand(request.Id));

            return result.Success(true, request.Id, null);
        }
    }
}
