using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommandHandler : IRequestHandler<DeleteAdvertiserCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public DeleteAdvertiserCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdvertiserDocument> advertiserMongoDB)
        {
            Mediator = mediator;
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(DeleteAdvertiserCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await AdvertiserMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == request.Id, 
                update: Builders<AdvertiserDocument>.Update.Set(f => f.DeletedAt, MechineDateTime.Now.DateTime)
            );
            return result.Success(true);
        }
    }
}
