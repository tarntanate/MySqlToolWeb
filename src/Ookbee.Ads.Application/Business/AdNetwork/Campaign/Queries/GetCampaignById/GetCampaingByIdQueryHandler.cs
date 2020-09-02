using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, HttpResult<CampaignDto>>
    {
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public GetCampaignByIdQueryHandler(AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<HttpResult<CampaignDto>> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await CampaignDbRepo.FirstAsync(
                selector: CampaignDto.Projection,
                filter: f => f.Id == request.Id && f.DeletedAt == null
            );

            var result = new HttpResult<CampaignDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Campaign '{request.Id}' doesn't exist.");
        }
    }
}
