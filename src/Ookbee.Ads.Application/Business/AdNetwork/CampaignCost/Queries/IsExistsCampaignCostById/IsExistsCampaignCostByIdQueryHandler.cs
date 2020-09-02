using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Queries.IsExistsCampaignCostById
{
    public class IsExistsCampaignCostByIdQueryHandler : IRequestHandler<IsExistsCampaignCostByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<CampaignCostEntity> CampaignCostDbRepo { get; }

        public IsExistsCampaignCostByIdQueryHandler(AdsDbRepository<CampaignCostEntity> advertiserDbRepo)
        {
            CampaignCostDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignCostByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await CampaignCostDbRepo.AnyAsync(f => f.Id == request.Id);

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Cost doesn't exist.");
        }
    }
}
