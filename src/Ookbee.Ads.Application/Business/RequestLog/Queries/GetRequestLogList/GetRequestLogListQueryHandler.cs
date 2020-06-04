using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Queries.GetRequestLogList
{
    public class GetRequestLogListQueryHandler : IRequestHandler<GetRequestLogListQuery, HttpResult<IEnumerable<RequestLogDto>>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<RequestLogDocument> RequestLogMongoDB { get; }

        public GetRequestLogListQueryHandler(
            IMediator mediator,
            AdsMongoRepository<RequestLogDocument> requestLogMongoDB)
        {
            Mediator = mediator;
            RequestLogMongoDB = requestLogMongoDB;
        }

        public async Task<HttpResult<IEnumerable<RequestLogDto>>> Handle(GetRequestLogListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<RequestLogDto>>> GetListMongoDB(GetRequestLogListQuery request)
        {
            var result = new HttpResult<IEnumerable<RequestLogDto>>();

            var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.CampaignId));
            if (!isExistsCampaignResult.Ok)
                return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);

            var items = await RequestLogMongoDB.FindAsync(
                filter: f => f.DeletedAt == null,
                sort: Builders<RequestLogDocument>.Sort.Descending(nameof(RequestLogDocument.UpdatedAt)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<RequestLogDto>>();
            return result.Success(data);
        }
    }
}
