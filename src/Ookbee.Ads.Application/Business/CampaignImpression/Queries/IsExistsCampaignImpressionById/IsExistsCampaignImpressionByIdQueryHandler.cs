using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Queries.IsExistsCampaignImpressionById
{
    public class IsExistsCampaignImpressionByIdQueryHandler : IRequestHandler<IsExistsCampaignImpressionByIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<CampaignImpressionEntity> CampaignImpressionEFCoreRepo { get; }

        public IsExistsCampaignImpressionByIdQueryHandler(AdsEFCoreRepository<CampaignImpressionEntity> advertiserEFCoreRepo)
        {
            CampaignImpressionEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignImpressionByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsCampaignImpressionByIdQuery request)
        {
            var result = new HttpResult<bool>();
            
            var isExists = await CampaignImpressionEFCoreRepo.AnyAsync(f => f.Id == request.Id);
            
            if (!isExists)
                return result.Fail(404, $"CampaignImpression '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
