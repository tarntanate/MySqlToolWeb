﻿using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdRevealList
{
    public class GetAdRevealListQueryHandler : IRequestHandler<GetAdRevealListQuery, HttpResult<IEnumerable<AdDto>>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public GetAdRevealListQueryHandler(AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdDto>>> Handle(GetAdRevealListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);
            predicate = predicate.And(f => f.Status == AdStatus.Preview || f.Status == AdStatus.Publish);

            if (request.AdUnitId.HasValue() && request.AdUnitId > 0)
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

            var result = new HttpResult<IEnumerable<AdDto>>();
            return result.Success(items);
        }
    }
}