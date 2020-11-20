using MediatR;
using Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupUnitIdCache;
using Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdUnitIdCache;
using Ookbee.Ads.Application.Services.Cache.Queries.GetAdUnitIdListCache;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteUnavailableAdUnitCache
{
    public class DeleteUnavailableAdUnitCacheCommandHandler : IRequestHandler<DeleteUnavailableAdUnitCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public DeleteUnavailableAdUnitCacheCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Unit> Handle(DeleteUnavailableAdUnitCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdUitIds = await Mediator.Send(new GetAdUnitIdListCacheQuery(), cancellationToken);
            if (getAdUitIds.IsSuccess)
            {
                foreach (var adUnitId in getAdUitIds.Data)
                {
                    var predicate = AdUnitCacheFilter.Available(adUnitId);
                    var isExists = await AdUnitDbRepo.AnyAsync(
                        filter: predicate
                    );
                    if (!isExists)
                    {
                        var adUnit = await AdUnitDbRepo.FirstAsync(
                            filter: f => f.Id == adUnitId,
                            selector: f => new { f.AdGroupId }
                        );
                        await Mediator.Send(new DeleteAdUnitIdCacheCommand(adUnitId), cancellationToken);
                        await Mediator.Send(new DeleteAdGroupUnitIdCacheCommand(adUnit.AdGroupId, adUnitId), cancellationToken);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
