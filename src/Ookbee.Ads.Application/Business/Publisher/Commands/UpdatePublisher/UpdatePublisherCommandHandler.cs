using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherByName;
using Ookbee.Ads.Common.Helpers;
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
            var document = Mapper.Map(request).ToANew<PublisherDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(PublisherDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsByIdResult = await Mediator.Send(new IsExistsPublisherByIdQuery(document.Id));
                if (!isExistsByIdResult.Ok)
                    return isExistsByIdResult;
                
                var isExistsByNameResult = await Mediator.Send(new IsExistsPublisherByNameQuery(document.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"Publisher '{document.Name}' already exists.");

                var now = MechineDateTime.Now;
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
