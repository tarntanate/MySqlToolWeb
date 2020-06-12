using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignCost.Queries.GetCampaignCostById
{
    public class GetCampaignCostByCampaignIdQueryHandler : IRequestHandler<GetCampaignCostByCampaignIdQuery, HttpResult<CampaignCostDto>>
    {
        private AdsDbRepository<CampaignCostEntity> CampaignCostDbRepo { get; }

        public GetCampaignCostByCampaignIdQueryHandler(AdsDbRepository<CampaignCostEntity> publisherDbRepo)
        {
            CampaignCostDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<CampaignCostDto>> Handle(GetCampaignCostByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<CampaignCostDto>> GetOnDb(GetCampaignCostByCampaignIdQuery request)
        {
            var result = new HttpResult<CampaignCostDto>();

            var item = await CampaignCostDbRepo.FirstAsync(filter: f => f.CampaignId == request.CampaignId);
            if (item == null)
                return result.Fail(404, $"CampaignCost '{request.CampaignId}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<CampaignCostDto>();

            return result.Success(data);
        }
    }
}
