using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.CreatePricingModel
{
    public class CreatePricingModelCommandHandler : IRequestHandler<CreatePricingModelCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public CreatePricingModelCommandHandler(
            IMediator mediator,
            AdsMongoRepository<PricingModelDocument> pricingModelMongoDB)
        {
            Mediator = mediator;
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreatePricingModelCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<PricingModelDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(PricingModelDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                document.EnabledFlag = true;
                await PricingModelMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
