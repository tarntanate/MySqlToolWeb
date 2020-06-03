using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Commands.CreateRequestLog
{
    public class CreateRequestLogCommandHandler : IRequestHandler<CreateRequestLogCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<RequestLogDocument> RequestLogMongoDB { get; }

        public CreateRequestLogCommandHandler(
            IMediator mediator,
            AdsMongoRepository<RequestLogDocument> requestLogMongoDB)
        {
            Mediator = mediator;
            RequestLogMongoDB = requestLogMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateRequestLogCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateRequestLogCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var document = Mapper.Map(request).ToANew<RequestLogDocument>();
                await RequestLogMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
