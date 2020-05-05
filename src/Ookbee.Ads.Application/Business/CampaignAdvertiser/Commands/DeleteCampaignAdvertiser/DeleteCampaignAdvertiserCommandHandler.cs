using MediatR;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByIdCampaignAdvertiser;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Commands.DeleteCampaignAdvertiser
{
    public class DeleteCampaignAdvertiserCommandHandler : IRequestHandler<DeleteCampaignAdvertiserCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> CampaignAdvertiserMongoDB { get; }

        public DeleteCampaignAdvertiserCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> campaignAdvertiserMongoDB)
        {
            Mediatr = mediatr;
            CampaignAdvertiserMongoDB = campaignAdvertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdCampaignAdvertiserCommand(id));
            if (!isExistsByNameResult.Data)
                return result.Fail(404, $"Advertiser '{id}' doesn't exist.");

            await CampaignAdvertiserMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
