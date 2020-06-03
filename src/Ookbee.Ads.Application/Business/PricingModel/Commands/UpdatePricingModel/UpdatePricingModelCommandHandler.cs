using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelById;
using Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.UpdatePricingModel
{
    public class UpdatePricingModelCommandHandler : IRequestHandler<UpdatePricingModelCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public UpdatePricingModelCommandHandler(
            IMediator mediator,
            AdsMongoRepository<PricingModelDocument> pricingModelMongoDB)
        {
            Mediator = mediator;
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdatePricingModelCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdatePricingModelCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var pricingModelResult = await Mediator.Send(new GetPricingModelByIdQuery(request.Id));
                if (!pricingModelResult.Ok)
                    return result.Fail(pricingModelResult.StatusCode, pricingModelResult.Message);

                var pricingModelByNameResult = await Mediator.Send(new GetPricingModelByNameQuery(request.Name));
                if (pricingModelByNameResult.Ok &&
                    pricingModelByNameResult.Data.Id != request.Id &&
                    pricingModelByNameResult.Data.Name == request.Name)
                    return result.Fail(409, $"PricingModel '{request.Name}' already exists.");

                var template = Mapper.Map(request).Over(pricingModelResult.Data);
                var document = Mapper.Map(template).ToANew<PricingModelDocument>();
                await PricingModelMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
