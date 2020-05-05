using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignDocument> CampaignMongoDB { get; }

        public DeleteCampaignCommandHandler(OokbeeAdsMongoDBRepository<CampaignDocument> campaignMongoDB)
        {
            CampaignMongoDB = campaignMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();
            await CampaignMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
