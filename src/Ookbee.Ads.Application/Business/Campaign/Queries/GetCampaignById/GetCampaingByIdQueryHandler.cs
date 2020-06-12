using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById
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
            return await GetOnDb(request);
        }

        private async Task<HttpResult<CampaignDto>> GetOnDb(GetCampaignByIdQuery request)
        {
            var result = new HttpResult<CampaignDto>();

            var item = await CampaignDbRepo.FirstAsync(
                selector: CampaignDto.Projection,
                filter: f => f.Id == request.Id && f.DeletedAt == null
            );

            if (item == null)
                return result.Fail(404, $"Campaign '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<CampaignDto>();

            return result.Success(data);
        }
    }
}
