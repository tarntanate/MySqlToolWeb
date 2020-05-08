using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItem.Commands.DeleteCampaignItem
{
    public class DeleteCampaignItemCommandHandler : IRequestHandler<DeleteCampaignItemCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignItemDocument> CampaignItemMongoDB { get; }

        public DeleteCampaignItemCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignItemDocument> campaignItemMongoDB)
        {
            Mediatr = mediatr;
            CampaignItemMongoDB = campaignItemMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignItemCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();
            await CampaignItemMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
