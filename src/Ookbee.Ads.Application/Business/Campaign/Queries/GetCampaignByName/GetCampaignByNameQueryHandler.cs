using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQueryHandler : IRequestHandler<GetCampaignByNameQuery, HttpResult<CampaignDto>>
    {
        private AdsEFCoreRepository<CampaignEntity> CampaignEFCoreRepo { get; }

        public GetCampaignByNameQueryHandler(AdsEFCoreRepository<CampaignEntity> campaignEFCoreRepo )
        {
            CampaignEFCoreRepo = campaignEFCoreRepo;
        }

        public async Task<HttpResult<CampaignDto>> Handle(GetCampaignByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<CampaignDto>> GetOnDb(GetCampaignByNameQuery request)
        {
            var result = new HttpResult<CampaignDto>();

            var item = await CampaignEFCoreRepo.FirstAsync(
                selector: CampaignDto.Projection,
                filter: f => f.Name == request.Name && f.DeletedAt == null
            );

            if (item == null)
                return result.Fail(404, $"Campaign '{request.Name}' doesn't exist.");

            var data = Mapper
                .Map(item)
                .ToANew<CampaignDto>();

            return result.Success(data);
        }
    }
}
