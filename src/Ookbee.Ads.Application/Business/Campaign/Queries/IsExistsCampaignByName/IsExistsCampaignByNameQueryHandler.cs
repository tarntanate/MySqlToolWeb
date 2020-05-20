using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignByName
{
    public class IsExistsCampaignByNameQueryHandler : IRequestHandler<IsExistsCampaignByNameQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<CampaignDocument> CampaignDocument { get; }

        public IsExistsCampaignByNameQueryHandler(AdsMongoRepository<CampaignDocument> campaignDocument)
        {
            CampaignDocument = campaignDocument;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(IsExistsCampaignByNameQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignDocument.AnyAsync(
                filter: f => f.Name == request.Name && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Campaign '{request.Name}' doesn't exist.");
        }
    }
}
