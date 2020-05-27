using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using Ookbee.RequestLogs.Application.Business.RequestLog.Commands.CreateRequestLog;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdBySlotId
{
    public class GetAdBySlotIdQueryHandler : IRequestHandler<GetAdBySlotIdQuery, HttpResult<AdItemDto>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public GetAdBySlotIdQueryHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<AdItemDto>> Handle(GetAdBySlotIdQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<AdItemDto>();

            var isExistsAdSlotById = await Mediator.Send(new IsExistsAdSlotByIdQuery(request.AdSlotId));
            if (!isExistsAdSlotById.Ok)
                return result.Fail(isExistsAdSlotById.StatusCode, result.Message);

            var createRequestLogCommand = Mapper.Map(request).ToANew<CreateRequestLogCommand>();
            var createRequestLogResult = await Mediator.Send(createRequestLogCommand);
            if (!createRequestLogResult.Ok)
                return result.Fail(createRequestLogResult.StatusCode, createRequestLogResult.Message);

            var data = new AdItemDto();
            data.RequestId = createRequestLogResult.Data;

            return result.Success(data);
        }
    }
}
