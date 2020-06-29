using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Queries.GetCampaignImpressionById
{
    public class GetCampaignImpressionByCampaignIdQueryHandler : IRequestHandler<GetCampaignImpressionByCampaignIdQuery, HttpResult<CampaignImpressionDto>>
    {
        private AdsDbRepository<CampaignImpressionEntity> CampaignImpressionDbRepo { get; }

        public GetCampaignImpressionByCampaignIdQueryHandler(AdsDbRepository<CampaignImpressionEntity> publisherDbRepo)
        {
            CampaignImpressionDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<CampaignImpressionDto>> Handle(GetCampaignImpressionByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<CampaignImpressionDto>> GetOnDb(GetCampaignImpressionByCampaignIdQuery request)
        {
            var result = new HttpResult<CampaignImpressionDto>();

            var item = await CampaignImpressionDbRepo.FirstAsync(
                selector: CampaignImpressionDto.Projection,
                filter: f => f.CampaignId == request.CampaignId);

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Impression doesn't exist.");
        }
    }
}
