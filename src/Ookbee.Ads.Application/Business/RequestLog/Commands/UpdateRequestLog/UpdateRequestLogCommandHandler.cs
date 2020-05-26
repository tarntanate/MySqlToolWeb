using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.RequestLog.Queries.IsExistsRequestLogById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Commands.UpdateRequestLog
{
    public class UpdateRequestLogCommandHandler : IRequestHandler<UpdateRequestLogCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<RequestLogDocument> RequestLogMongoDB { get; }

        public UpdateRequestLogCommandHandler(
            IMediator mediator,
            AdsMongoRepository<RequestLogDocument> requestLogMongoDB)
        {
            Mediator = mediator;
            RequestLogMongoDB = requestLogMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateRequestLogCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateRequestLogCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsResult = await Mediator.Send(new IsExistsRequestLogByIdQuery(request.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<RequestLogDocument>();
                document.UpdatedDate = now.DateTime;
                await RequestLogMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
