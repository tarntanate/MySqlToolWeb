using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQueryHandler : IRequestHandler<IsExistsCampaignByIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<CampaignEntity> CampaignEFCoreRepo { get; }

        public IsExistsCampaignByIdQueryHandler(AdsEFCoreRepository<CampaignEntity> campaignEFCoreRepo)
        {
            CampaignEFCoreRepo = campaignEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsCampaignByIdQuery request)
        {
            var result = new HttpResult<bool>();
            
            var isExists = await CampaignEFCoreRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"Campaign '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
