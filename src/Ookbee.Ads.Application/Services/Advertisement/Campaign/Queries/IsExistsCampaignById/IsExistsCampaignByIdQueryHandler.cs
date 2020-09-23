using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQueryHandler : IRequestHandler<IsExistsCampaignByIdQuery, Response<bool>>
    {
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public IsExistsCampaignByIdQueryHandler(
            AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await CampaignDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Data doesn't exist.");
        }
    }
}
