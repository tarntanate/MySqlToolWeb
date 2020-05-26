using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherByName;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Common;
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
                var isExistsByIdResult = await Mediator.Send(new IsExistsPublisherByIdQuery(request.Id));
                if (!isExistsByIdResult.Ok)
                    return isExistsByIdResult;

                var publisherResult = await Mediator.Send(new GetPublisherByNameQuery(request.Name));
                if (publisherResult.Ok &&
                    publisherResult.Data.Id != request.Id &&
                    publisherResult.Data.Name == request.Name)
                    return result.Fail(409, $"Publisher '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<PublisherDocument>();
                document.UpdatedDate = now.DateTime;
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
