using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelByName;
using Ookbee.Ads.Common;
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
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreatePricingModelCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediator.Send(new IsExistsPricingModelByNameQuery(request.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"PricingModel '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<PricingModelDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
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
