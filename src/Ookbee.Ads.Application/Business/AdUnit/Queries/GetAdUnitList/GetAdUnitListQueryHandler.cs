using AgileObjects.AgileMapper;
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

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList
{
    public class GetAdUnitListQueryHandler : IRequestHandler<GetAdUnitListQuery, HttpResult<IEnumerable<AdUnitDto>>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public GetAdUnitListQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdUnitDto>>> Handle(GetAdUnitListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<AdUnitDto>>> GetListOnDb(GetAdUnitListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdUnitDto>>();

            var predicate = PredicateBuilder.True<AdUnitEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdUnitTypeId.HasValue() && request.AdUnitTypeId > 0)
                predicate = predicate.And(f => f.AdUnitTypeId == request.AdUnitTypeId);

            if (request.PublisherId.HasValue() && request.PublisherId > 0)
                predicate = predicate.And(f => f.PublisherId == request.PublisherId);

            var items = await AdUnitDbRepo.FindAsync(
                selector: AdUnitDto.Projection,
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var data = Mapper
                .Map(items)
                .ToANew<IEnumerable<AdUnitDto>>();

            return result.Success(data);
        }
    }
}
