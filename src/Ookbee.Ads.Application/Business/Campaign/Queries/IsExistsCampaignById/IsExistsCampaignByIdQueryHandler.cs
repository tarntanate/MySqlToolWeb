using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQueryHandler : IRequestHandler<IsExistsCampaignByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public IsExistsCampaignByIdQueryHandler(AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsCampaignByIdQuery request)
        {
            var result = new HttpResult<bool>();
            
            var isExists = await CampaignDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"Campaign '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
