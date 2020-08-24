using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignCost.Queries.IsExistsCampaignCostByCampaignId
{
    public class IsExistsCampaignCostByCampaignIdQueryHandler : IRequestHandler<IsExistsCampaignCostByCampaignIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<CampaignCostEntity> CampaignCostDbRepo { get; }

        public IsExistsCampaignCostByCampaignIdQueryHandler(AdsDbRepository<CampaignCostEntity> advertiserDbRepo)
        {
            CampaignCostDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignCostByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await CampaignCostDbRepo.AnyAsync(f => f.CampaignId == request.CampaignId);

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Cost doesn't exist.");
        }
    }
}
