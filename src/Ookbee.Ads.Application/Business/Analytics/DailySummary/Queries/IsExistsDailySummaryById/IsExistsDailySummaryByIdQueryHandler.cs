using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class IsExistsDailySummaryByIdQueryHandler : IRequestHandler<IsExistsDailySummaryByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<DailySummaryDocument> DailySummaryMongoDB { get; }

        public IsExistsDailySummaryByIdQueryHandler(AdsMongoRepository<DailySummaryDocument> dailySummaryMongoDB)
        {
            DailySummaryMongoDB = dailySummaryMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsDailySummaryByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(IsExistsDailySummaryByIdQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await DailySummaryMongoDB.AnyAsync(
                filter: f => f.Id == request.Id &&
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"DailySummary '{request.Id}' doesn't exist.");
        }
    }
}
