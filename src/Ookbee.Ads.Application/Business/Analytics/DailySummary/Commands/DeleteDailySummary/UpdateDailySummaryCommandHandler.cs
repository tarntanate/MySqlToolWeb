using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class DeleteDailySummaryCommandHandler : IRequestHandler<DeleteDailySummaryCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<DailySummaryDocument> DailySummaryMongoDB { get; }

        public DeleteDailySummaryCommandHandler(
            IMediator mediator,
            AdsMongoRepository<DailySummaryDocument> dailySummaryMongoDB)
        {
            Mediator = mediator;
            DailySummaryMongoDB = dailySummaryMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteDailySummaryCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(DeleteDailySummaryCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsDailySummaryByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await DailySummaryMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == request.Id,
                update: Builders<DailySummaryDocument>.Update.Set(f => f.EnabledFlag, false)
            );
            return result.Success(true);
        }
    }
}
