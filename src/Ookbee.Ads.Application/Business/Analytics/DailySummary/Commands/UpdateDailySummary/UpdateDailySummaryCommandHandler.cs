using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class UpdateDailySummaryCommandHandler : IRequestHandler<UpdateDailySummaryCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<DailySummaryDocument> DailySummaryMongoDB { get; }

        public UpdateDailySummaryCommandHandler(
            IMediator mediator,
            AdsMongoRepository<DailySummaryDocument> dailySummaryMongoDB)
        {
            Mediator = mediator;
            DailySummaryMongoDB = dailySummaryMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateDailySummaryCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateDailySummaryCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsByIdResult = await Mediator.Send(new IsExistsDailySummaryByIdQuery(request.Id));
                if (!isExistsByIdResult.Ok)
                    return isExistsByIdResult;

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<DailySummaryDocument>();
                document.UpdatedDate = now.DateTime;
                await DailySummaryMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
