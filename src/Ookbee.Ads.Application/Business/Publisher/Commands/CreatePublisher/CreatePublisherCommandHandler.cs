using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherByName;
using Ookbee.Ads.Common;
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
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreatePublisherCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediator.Send(new IsExistsPublisherByNameQuery(request.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"Publisher '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<PublisherDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
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
