using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherByName;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<PublisherDocument> PublisherMongoDB { get; }

        public CreatePublisherCommandHandler(
            IMediator mediator,
            AdsMongoRepository<PublisherDocument> publisherMongoDB)
        {
            Mediator = mediator;
            PublisherMongoDB = publisherMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<PublisherDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(PublisherDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediator.Send(new IsExistsPublisherByNameQuery(document.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"Publisher '{document.Name}' already exists.");

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                document.EnabledFlag = true;
                await PublisherMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
