﻿using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdIdList
{
    public class GetAdIdListQueryHandler : IRequestHandler<GetAdIdListQuery, HttpResult<IEnumerable<long>>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public GetAdIdListQueryHandler(AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<IEnumerable<long>>> Handle(GetAdIdListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<long>>> GetListOnDb(GetAdIdListQuery request)
        {
            var result = new HttpResult<IEnumerable<long>>();

            var predicate = PredicateBuilder.True<AdEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdUnitId.HasValue() && request.AdUnitId > 0)
                predicate = predicate.And(f => f.AdUnitId == request.AdUnitId);

            if (request.CampaignId.HasValue() && request.CampaignId > 0)
                predicate = predicate.And(f => f.CampaignId == request.CampaignId);

            var items = await AdDbRepo.FindAsync(
                selector: f => new { Id = f.Id },
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            return result.Success(items.Select(f => f.Id));
        }
    }
}