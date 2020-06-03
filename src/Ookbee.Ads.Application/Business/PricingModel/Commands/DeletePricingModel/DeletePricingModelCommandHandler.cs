using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.DeletePricingModel
{
    public class DeletePricingModelCommandHandler : IRequestHandler<DeletePricingModelCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public DeletePricingModelCommandHandler(
            IMediator mediator,
            AdsMongoRepository<PricingModelDocument> pricingModelMongoDB)
        {
            Mediator = mediator;
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeletePricingModelCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(DeletePricingModelCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsPricingModelByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await PricingModelMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == request.Id, 
                update: Builders<PricingModelDocument>.Update.Set(f => f.DeletedAt, MechineDateTime.Now.DateTime)
            );
            return result.Success(true);
        }
    }
}
