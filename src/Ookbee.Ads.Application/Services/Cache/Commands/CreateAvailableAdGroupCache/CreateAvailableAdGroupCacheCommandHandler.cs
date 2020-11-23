using MediatR;
using Ookbee.Ads.Application.Services.Cache.Commands.CreateAdGroupIdCache;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAvailableAdGroupCache
{
    public class CreateAvailableAdGroupCacheCommandHandler : IRequestHandler<CreateAvailableAdGroupCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public CreateAvailableAdGroupCacheCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Unit> Handle(CreateAvailableAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var isNext = false;
            do
            {
                var predicate = AdGroupCacheFilter.Available();
                var items = await AdGroupDbRepo.FindAsync(
                    filter: predicate,
                    selector: f => new { f.Id },
                    start: start,
                    length: length
                );
                if (items.HasValue())
                {
                    var adGroupIds = items.Select(x => x.Id).ToList();
                    await Mediator.Send(new CreateAdGroupIdCacheCommand(adGroupIds), cancellationToken);
                }
                isNext = items.Count() == length ? true : false;
                start += length;
            }
            while (isNext);

            return Unit.Value;
        }
    }
}
