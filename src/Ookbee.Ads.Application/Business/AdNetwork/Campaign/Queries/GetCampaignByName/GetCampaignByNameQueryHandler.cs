using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQueryHandler : IRequestHandler<GetCampaignByNameQuery, HttpResult<CampaignDto>>
    {
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public GetCampaignByNameQueryHandler(AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<HttpResult<CampaignDto>> Handle(GetCampaignByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await CampaignDbRepo.FirstAsync(
                selector: CampaignDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            var result = new HttpResult<CampaignDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Campaign '{request.Name}' doesn't exist.");
        }
    }
}
