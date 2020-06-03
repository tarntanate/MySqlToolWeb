using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<CampaignDocument> CampaignMongoDB { get; }

        public DeleteCampaignCommandHandler(
            IMediator mediator,
            AdsMongoRepository<CampaignDocument> campaignMongoDB)
        {
            Mediator = mediator;
            CampaignMongoDB = campaignMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(DeleteCampaignCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await CampaignMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == request.Id, 
                update: Builders<CampaignDocument>.Update.Set(f => f.DeletedAt, MechineDateTime.Now.DateTime)
            );
            return result.Success(true);
        }
    }
}
