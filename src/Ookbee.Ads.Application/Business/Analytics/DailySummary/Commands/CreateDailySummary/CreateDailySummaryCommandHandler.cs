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
    public class CreateDailySummaryCommandHandler : IRequestHandler<CreateDailySummaryCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<DailySummaryDocument> DailySummaryMongoDB { get; }

        public CreateDailySummaryCommandHandler(
            IMediator mediator,
            AdsMongoRepository<DailySummaryDocument> dailySummaryMongoDB)
        {
            Mediator = mediator;
            DailySummaryMongoDB = dailySummaryMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateDailySummaryCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateDailySummaryCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<DailySummaryDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await DailySummaryMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
