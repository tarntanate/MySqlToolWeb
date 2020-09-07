﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Queries.IsExistsCampaignImpressionById
{
    public class IsExistsCampaignImpressionByIdQueryHandler : IRequestHandler<IsExistsCampaignImpressionByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<CampaignImpressionEntity> CampaignImpressionDbRepo { get; }

        public IsExistsCampaignImpressionByIdQueryHandler(AdsDbRepository<CampaignImpressionEntity> advertiserDbRepo)
        {
            CampaignImpressionDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignImpressionByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await CampaignImpressionDbRepo.AnyAsync(f => f.Id == request.Id);

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Impression doesn't exist.");
        }
    }
}