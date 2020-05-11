using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsByIdAdvertiser;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommandHandler : IRequestHandler<DeleteAdvertiserCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public DeleteAdvertiserCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<AdvertiserDocument> advertiserMongoDB)
        {
            Mediatr = mediatr;
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdAdvertiserCommand(id));
            if (!isExistsByNameResult.Data)
                return result.Fail(404, $"Advertiser '{id}' doesn't exist.");

            await AdvertiserMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
