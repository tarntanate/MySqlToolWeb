using AgileObjects.AgileMapper;
using MediatR;
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
                var dailySummaryResult = await Mediator.Send(new GetDailySummaryByIdQuery(request.Id));
                if (!dailySummaryResult.Ok)
                    return result.Fail(dailySummaryResult.StatusCode, dailySummaryResult.Message);

                var template = Mapper.Map(request).Over(dailySummaryResult.Data);
                var document = Mapper.Map(template).ToANew<DailySummaryDocument>();
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
