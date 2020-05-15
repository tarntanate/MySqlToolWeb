using MediatR;
using Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById;
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
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsPricingModelByIdQuery(id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await PricingModelMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
