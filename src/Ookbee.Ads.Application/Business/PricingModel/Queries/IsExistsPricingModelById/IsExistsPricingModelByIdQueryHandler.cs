using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById
{
    public class IsExistsPricingModelByIdQueryHandler : IRequestHandler<IsExistsPricingModelByIdQuery, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public IsExistsPricingModelByIdQueryHandler(OokbeeAdsMongoDBRepository<PricingModelDocument> pricingModelMongoDB)
        {
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsPricingModelByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await PricingModelMongoDB.AnyAsync(filter: f => f.Id == id);
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"PricingModel '{id}' doesn't exist.");
        }
    }
}
