using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotById;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileList;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using Ookbee.RequestLogs.Application.Business.RequestLog.Commands.CreateRequestLog;
using System.Linq;
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

            var isExistsAdSlotByIdResult = await Mediator.Send(new IsExistsAdSlotByIdQuery(request.AdSlotId));
            if (!isExistsAdSlotByIdResult.Ok)
                return result.Fail(isExistsAdSlotByIdResult.StatusCode, isExistsAdSlotByIdResult.Message);

            var createRequestLogCommand = Mapper.Map(request).ToANew<CreateRequestLogCommand>();
            var createRequestLogResult = await Mediator.Send(createRequestLogCommand);
            if (!createRequestLogResult.Ok)
                return result.Fail(createRequestLogResult.StatusCode, createRequestLogResult.Message);

            var adListResult = await Mediator.Send(new GetAdListQuery(request.AdSlotId, null, 0, 100));
            if (!adListResult.Ok)
                return result.Fail(adListResult.StatusCode, adListResult.Message);

            var ad = adListResult.Data.FirstOrDefault();

            var mediaFileListResult = await Mediator.Send(new GetMediaFileListQuery("5ec616d219a8622290b9d5f8", 0, 100));
            if (!mediaFileListResult.Ok)
                return result.Fail(mediaFileListResult.StatusCode, mediaFileListResult.Message);

            var data = Mapper.Map(ad).ToANew<AdItemDto>();
            data.MediaFiles = mediaFileListResult.Data;

            return result.Success(data);
        }
    }
}
