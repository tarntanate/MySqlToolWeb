using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.RequestLog.Queries.IsExistsRequestLogById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Commands.DeleteRequestLog
{
    public class DeleteRequestLogCommandHandler : IRequestHandler<DeleteRequestLogCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<RequestLogDocument> RequestLogMongoDB { get; }

        public DeleteRequestLogCommandHandler(
            IMediator mediator,
            AdsMongoRepository<RequestLogDocument> requestLogMongoDB)
        {
            Mediator = mediator;
            RequestLogMongoDB = requestLogMongoDB;
        }


        public async Task<HttpResult<bool>> Handle(DeleteRequestLogCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(DeleteRequestLogCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsRequestLogByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await RequestLogMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == request.Id,
                update: Builders<RequestLogDocument>.Update.Set(f => f.EnabledFlag, false)
            );
            return result.Success(true);
        }
    }
}
