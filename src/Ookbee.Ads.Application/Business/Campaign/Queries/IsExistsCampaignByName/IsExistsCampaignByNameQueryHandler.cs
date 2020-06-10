﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignByName
{
    public class IsExistsCampaignByNameQueryHandler : IRequestHandler<IsExistsCampaignByNameQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<CampaignEntity> CampaignEFCoreRepo { get; }

        public IsExistsCampaignByNameQueryHandler(AdsEFCoreRepository<CampaignEntity> campaignEFCoreRepo)
        {
            CampaignEFCoreRepo = campaignEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsCampaignByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await CampaignEFCoreRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"Campaign '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
