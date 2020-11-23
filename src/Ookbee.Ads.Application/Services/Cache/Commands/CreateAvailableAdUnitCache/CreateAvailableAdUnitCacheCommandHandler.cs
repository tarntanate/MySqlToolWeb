using MediatR;
using Ookbee.Ads.Application.Services.Cache.Commands.CreateAdGroupUnitIdCache;
using Ookbee.Ads.Application.Services.Cache.Commands.CreateAdUnitIdCache;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAvailableAdUnitCache
{
    public class CreateAvailableAdUnitCacheCommandHandler : IRequestHandler<CreateAvailableAdUnitCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public CreateAvailableAdUnitCacheCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Unit> Handle(CreateAvailableAdUnitCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var predicate = AdUnitCacheFilter.Available();
                var items = await AdUnitDbRepo.FindAsync(
                    filter: predicate,
                    selector: f => new { f.Id, f.AdGroupId },
                    start: start,
                    length: length
                );
                if (items.HasValue())
                {
                    var adGroupIds = items.Select(item => item.AdGroupId).Distinct();
                    var adUnitIds = items.Select(item => item.Id).ToList();
                    
                    await Mediator.Send(new CreateAdUnitIdCacheCommand(adUnitIds), cancellationToken);
                    foreach(var adGroupId in adGroupIds)
                    {
                        adUnitIds = items.Where(item => item.AdGroupId == adGroupId).Select(item => item.Id).ToList();
                        await Mediator.Send(new CreateAdGroupUnitIdCacheCommand(adGroupId, adUnitIds), cancellationToken);
                    }
                }
                next = items.Count() == length ? true : false;
                start += length;
            }
            while (next);

            return Unit.Value;
        }
    }
}
