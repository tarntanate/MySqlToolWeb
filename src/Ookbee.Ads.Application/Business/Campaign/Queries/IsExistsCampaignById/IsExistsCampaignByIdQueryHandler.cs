using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQueryHandler : IRequestHandler<IsExistsCampaignByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<CampaignDocument> CampaignDocument { get; }

        public IsExistsCampaignByIdQueryHandler(AdsMongoRepository<CampaignDocument> campaignDocument)
        {
            CampaignDocument = campaignDocument;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(IsExistsCampaignByIdQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignDocument.AnyAsync(
                filter: f => f.Id == request.Id && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Campaign '{request.Id}' doesn't exist.");
        }
    }
}
