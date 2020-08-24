using MediatR;
using Ookbee.Ads.Application.Business.AdGroupCache.Commands.DeleteAdGroupCache;
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
            await Mediator.Send(new DeleteAdGroupCacheCommand(request.Id), cancellationToken);
            await AdGroupDbRepo.DeleteAsync(request.Id);
            await AdGroupDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
