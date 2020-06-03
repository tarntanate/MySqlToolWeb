using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById
{
    public class IsExistsPricingModelByIdQueryHandler : IRequestHandler<IsExistsPricingModelByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public IsExistsPricingModelByIdQueryHandler(AdsMongoRepository<PricingModelDocument> pricingModelMongoDB)
        {
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsPricingModelByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(IsExistsPricingModelByIdQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await PricingModelMongoDB.AnyAsync(
                filter: f => f.Id == request.Id && 
                             f.DeletedAt == null
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"PricingModel '{request.Id}' doesn't exist.");
        }
    }
}
