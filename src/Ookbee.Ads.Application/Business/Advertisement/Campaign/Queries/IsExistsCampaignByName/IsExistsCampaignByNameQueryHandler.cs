using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Queries.IsExistsCampaignByName
{
    public class IsExistsCampaignByNameQueryHandler : IRequestHandler<IsExistsCampaignByNameQuery, Response<bool>>
    {
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public IsExistsCampaignByNameQueryHandler(AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsCampaignByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await CampaignDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Campaign '{request.Name}' doesn't exist.");
        }
    }
}
