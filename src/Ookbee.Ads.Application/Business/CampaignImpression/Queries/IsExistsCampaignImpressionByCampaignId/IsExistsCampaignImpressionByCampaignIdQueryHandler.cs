using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Queries.IsExistsCampaignImpressionByCampaignId
{
    public class IsExistsCampaignImpressionByCampaignIdQueryHandler : IRequestHandler<IsExistsCampaignImpressionByCampaignIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<CampaignImpressionEntity> CampaignImpressionDbRepo { get; }

        public IsExistsCampaignImpressionByCampaignIdQueryHandler(AdsDbRepository<CampaignImpressionEntity> advertiserDbRepo)
        {
            CampaignImpressionDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignImpressionByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await CampaignImpressionDbRepo.AnyAsync(f => f.CampaignId == request.CampaignId);

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Impression doesn't exist.");
        }
    }
}
