using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQueryHandler : IRequestHandler<GetAdListQuery, HttpResult<IEnumerable<AdDto>>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public GetAdListQueryHandler(AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdDto>>> Handle(GetAdListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<AdDto>>> GetListOnDb(GetAdListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdDto>>();

            var predicate = PredicateBuilder.True<AdEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdUnitId.HasValue() && request.CampaignId > 0)
                predicate = predicate.And(f => f.AdUnitId == request.AdUnitId);

            if (request.CampaignId.HasValue() && request.CampaignId > 0)
                predicate = predicate.And(f => f.CampaignId == request.CampaignId);

            var items = await AdDbRepo.FindAsync(
                selector: AdDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            return result.Success(items);
        }
    }
}
