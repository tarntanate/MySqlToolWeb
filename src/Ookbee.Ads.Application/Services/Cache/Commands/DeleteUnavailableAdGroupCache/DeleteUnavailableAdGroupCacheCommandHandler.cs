using MediatR;
using Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupIdCache;
using Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupUnitIdKeyCache;
using Ookbee.Ads.Application.Services.Cache.Queries.GetAdGroupIdListCache;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteUnavailableAdGroupCache
{
    public class DeleteUnavailableAdGroupCacheCommandHandler : IRequestHandler<DeleteUnavailableAdGroupCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public DeleteUnavailableAdGroupCacheCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Unit> Handle(DeleteUnavailableAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupIds = await Mediator.Send(new GetAdGroupIdListCacheQuery(), cancellationToken);
            if (getAdGroupIds.IsSuccess)
            {
                foreach (var adGroupId in getAdGroupIds.Data)
                {
                    var predicate = AdGroupCacheFilter.Available(adGroupId);
                    var isExists = await AdGroupDbRepo.AnyAsync(
                        filter: predicate
                    );
                    if (!isExists)
                    {
                        await Mediator.Send(new DeleteAdGroupIdCacheCommand(adGroupId), cancellationToken);
                        await Mediator.Send(new DeleteAdGroupUnitIdKeyCacheCommand(adGroupId), cancellationToken);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
