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

namespace Ookbee.Ads.Application.Business.ActivityLog.Queries.GetActivityLogList
{
    public class GetActivityLogListQueryHandler : IRequestHandler<GetActivityLogListQuery, HttpResult<IEnumerable<ActivityLogDto>>>
    {
        private AdsDbRepository<ActivityLogEntity> ActivityLogDbRepo { get; }

        public GetActivityLogListQueryHandler(AdsDbRepository<ActivityLogEntity> publisherDbRepo)
        {
            ActivityLogDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<IEnumerable<ActivityLogDto>>> Handle(GetActivityLogListQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<IEnumerable<ActivityLogDto>>();

            var predicate = PredicateBuilder.True<ActivityLogEntity>();

            if (request.UserId.HasValue())
                predicate = predicate.And(f => f.UserId == request.UserId);

            if (request.ObjectName.HasValue())
                predicate = predicate.And(f => f.ObjectType == request.ObjectName);

            var items = await ActivityLogDbRepo.FindAsync(
                filter: predicate,
                selector: ActivityLogDto.Projection,
                orderBy: f => f.OrderByDescending(o => o.CreatedAt),
                start: request.Start,
                length: request.Length
            );

            return result.Success(items);
        }
    }
}
