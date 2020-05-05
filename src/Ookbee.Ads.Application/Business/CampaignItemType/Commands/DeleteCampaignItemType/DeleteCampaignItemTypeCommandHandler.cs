using MediatR;
using Ookbee.Ads.Application.Business.CampaignItemType.Queries.IsExistsByIdCampaignItemType;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Commands.DeleteCampaignItemType
{
    public class DeleteCampaignItemTypeCommandHandler : IRequestHandler<DeleteCampaignItemTypeCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> CampaignItemTypeMongoDB { get; }

        public DeleteCampaignItemTypeCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> campaignItemTypeMongoDB)
        {
            Mediatr = mediatr;
            CampaignItemTypeMongoDB = campaignItemTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignItemTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdCampaignItemTypeCommand(id));
            if (!isExistsByNameResult.Data)
                return result.Fail(404, $"ItemType '{id}' doesn't exist.");

            await CampaignItemTypeMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
