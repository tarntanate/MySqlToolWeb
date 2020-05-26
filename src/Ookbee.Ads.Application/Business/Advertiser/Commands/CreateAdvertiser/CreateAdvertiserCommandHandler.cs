using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandHandler : IRequestHandler<CreateAdvertiserCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public CreateAdvertiserCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdvertiserDocument> advertiserMongoDB)
        {
            Mediator = mediator;
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateAdvertiserCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediator.Send(new IsExistsAdvertiserByNameQuery(request.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"Advertiser '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<AdvertiserDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await AdvertiserMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
