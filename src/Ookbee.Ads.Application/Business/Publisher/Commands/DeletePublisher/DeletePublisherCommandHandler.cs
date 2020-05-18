using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<PublisherDocument> PublisherMongoDB { get; }

        public DeletePublisherCommandHandler(
            IMediator mediator,
            AdsMongoRepository<PublisherDocument> publisherMongoDB)
        {
            Mediator = mediator;
            PublisherMongoDB = publisherMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsPublisherByIdQuery(id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await PublisherMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == id, 
                update: Builders<PublisherDocument>.Update.Set(f => f.EnabledFlag, false)
            );
            return result.Success(true);
        }
    }
}
