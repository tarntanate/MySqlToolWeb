using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<PublisherDocument> PublisherMongoDB { get; }

        public UpdatePublisherCommandHandler(
            IMediator mediator,
            AdsMongoRepository<PublisherDocument> publisherMongoDB)
        {
            Mediator = mediator;
            PublisherMongoDB = publisherMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdatePublisherCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var publisherResult = await Mediator.Send(new GetPublisherByIdQuery(request.Id));
                if (!publisherResult.Ok)
                    return result.Fail(publisherResult.StatusCode, publisherResult.Message);

                var publisherByNameResult = await Mediator.Send(new GetPublisherByNameQuery(request.Name));
                if (publisherByNameResult.Ok &&
                    publisherByNameResult.Data.Id != request.Id &&
                    publisherByNameResult.Data.Name == request.Name)
                    return result.Fail(409, $"Publisher '{request.Name}' already exists.");

                var template = Mapper.Map(request).Over(publisherResult.Data);
                var document = Mapper.Map(template).ToANew<PublisherDocument>();
                await PublisherMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
