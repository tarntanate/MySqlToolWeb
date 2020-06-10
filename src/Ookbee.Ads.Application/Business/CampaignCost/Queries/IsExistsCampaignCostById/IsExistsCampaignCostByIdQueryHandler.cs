using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignCost.Queries.IsExistsCampaignCostById
{
    public class IsExistsCampaignCostByIdQueryHandler : IRequestHandler<IsExistsCampaignCostByIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<CampaignCostEntity> CampaignCostEFCoreRepo { get; }

        public IsExistsCampaignCostByIdQueryHandler(AdsEFCoreRepository<CampaignCostEntity> advertiserEFCoreRepo)
        {
            CampaignCostEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignCostByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsCampaignCostByIdQuery request)
        {
            var result = new HttpResult<bool>();
            
            var isExists = await CampaignCostEFCoreRepo.AnyAsync(f => f.Id == request.Id);
            
            if (!isExists)
                return result.Fail(404, $"CampaignCost '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
