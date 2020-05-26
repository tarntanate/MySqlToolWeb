using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class GetDailySummaryByIdQueryHandler : IRequestHandler<GetDailySummaryByIdQuery, HttpResult<DailySummaryDto>>
    {
        private AdsMongoRepository<DailySummaryDocument> DailySummaryMongoDB { get; }

        public GetDailySummaryByIdQueryHandler(AdsMongoRepository<DailySummaryDocument> dailySummaryMongoDB)
        {
            DailySummaryMongoDB = dailySummaryMongoDB;
        }

        public async Task<HttpResult<DailySummaryDto>> Handle(GetDailySummaryByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<DailySummaryDto>> GetOnMongo(GetDailySummaryByIdQuery request)
        {
            var result = new HttpResult<DailySummaryDto>();
            var item = await DailySummaryMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == request.Id &&
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"DailySummary '{request.Id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<DailySummaryDto>();
            return result.Success(data);
        }
    }
}
