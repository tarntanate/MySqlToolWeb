using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignCost.Queries.IsExistsCampaignCostByCampaignId
{
    public class IsExistsCampaignCostByCampaignIdQueryHandler : IRequestHandler<IsExistsCampaignCostByCampaignIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<CampaignCostEntity> CampaignCostEFCoreRepo { get; }

        public IsExistsCampaignCostByCampaignIdQueryHandler(AdsEFCoreRepository<CampaignCostEntity> advertiserEFCoreRepo)
        {
            CampaignCostEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignCostByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsCampaignCostByCampaignIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await CampaignCostEFCoreRepo.AnyAsync(f => f.CampaignId == request.CampaignId);
            
            if (!isExists)
                return result.Fail(404, $"CampaignCost '{request.CampaignId}' doesn't exist.");
            return result.Success(true);
        }
    }
}
