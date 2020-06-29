using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQueryHandler : IRequestHandler<GetCampaignListQuery, HttpResult<IEnumerable<CampaignDto>>>
    {
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public GetCampaignListQueryHandler(AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<HttpResult<IEnumerable<CampaignDto>>> Handle(GetCampaignListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<CampaignDto>>> GetListOnDb(GetCampaignListQuery request)
        {
            var result = new HttpResult<IEnumerable<CampaignDto>>();

            var predicate = PredicateBuilder.True<CampaignEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdvertiserId.HasValue() && request.AdvertiserId > 0)
                predicate = predicate.And(f => f.AdvertiserId == request.AdvertiserId);

            if (request.PricingModel.HasValue() && Enum.TryParse<PricingModel>(request.PricingModel, true, out var pricingModel))
                predicate = predicate.And(f => f.PricingModel == pricingModel);

            var items = await CampaignDbRepo.FindAsync(
                selector: CampaignDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            return result.Success(items);
        }
    }
}
