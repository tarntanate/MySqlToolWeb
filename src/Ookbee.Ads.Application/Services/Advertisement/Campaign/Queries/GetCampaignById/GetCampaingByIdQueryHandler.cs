using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, Response<CampaignDto>>
    {
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public GetCampaignByIdQueryHandler(
            AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<Response<CampaignDto>> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await CampaignDbRepo.FirstAsync(
                selector: CampaignDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<CampaignDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Data not found.");
        }
    }
}
