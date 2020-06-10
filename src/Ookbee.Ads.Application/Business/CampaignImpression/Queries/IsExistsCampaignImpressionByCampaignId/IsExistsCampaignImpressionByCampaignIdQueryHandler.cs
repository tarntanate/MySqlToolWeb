using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Queries.IsExistsCampaignImpressionByCampaignId
{
    public class IsExistsCampaignImpressionByCampaignIdQueryHandler : IRequestHandler<IsExistsCampaignImpressionByCampaignIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<CampaignImpressionEntity> CampaignImpressionEFCoreRepo { get; }

        public IsExistsCampaignImpressionByCampaignIdQueryHandler(AdsEFCoreRepository<CampaignImpressionEntity> advertiserEFCoreRepo)
        {
            CampaignImpressionEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignImpressionByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsCampaignImpressionByCampaignIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await CampaignImpressionEFCoreRepo.AnyAsync(f => f.CampaignId == request.CampaignId);
            
            if (!isExists)
                return result.Fail(404, $"CampaignImpression '{request.CampaignId}' doesn't exist.");
            return result.Success(true);
        }
    }
}
