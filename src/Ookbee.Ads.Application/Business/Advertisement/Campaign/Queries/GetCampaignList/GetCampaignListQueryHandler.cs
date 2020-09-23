using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQueryHandler : IRequestHandler<GetCampaignListQuery, Response<IEnumerable<CampaignDto>>>
    {
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public GetCampaignListQueryHandler(AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<Response<IEnumerable<CampaignDto>>> Handle(GetCampaignListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<CampaignEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdvertiserId.HasValue() && request.AdvertiserId > 0)
                predicate = predicate.And(f => f.AdvertiserId == request.AdvertiserId);

            var items = await CampaignDbRepo.FindAsync(
                selector: CampaignDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<CampaignDto>>();
            return result.Success(items);
        }
    }
}
