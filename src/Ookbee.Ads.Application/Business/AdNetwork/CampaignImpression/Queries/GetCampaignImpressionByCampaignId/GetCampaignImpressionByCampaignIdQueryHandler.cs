using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Queries.GetCampaignImpressionById
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
            var item = await CampaignImpressionDbRepo.FirstAsync(
                selector: CampaignImpressionDto.Projection,
                filter: f => f.CampaignId == request.CampaignId);

            var result = new HttpResult<CampaignImpressionDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Impression doesn't exist.");
        }
    }
}
