using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelByName;
using Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById;
using Ookbee.Ads.Common;
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
                var isExistsResult = await Mediator.Send(new IsExistsPricingModelByIdQuery(request.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var pricingModelResult = await Mediator.Send(new GetPricingModelByNameQuery(request.Name));
                if (pricingModelResult.Ok &&
                    pricingModelResult.Data.Id != request.Id &&
                    pricingModelResult.Data.Name == request.Name)
                    return result.Fail(409, $"PricingModel '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<PricingModelDocument>();
                document.UpdatedDate = now.DateTime;
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
