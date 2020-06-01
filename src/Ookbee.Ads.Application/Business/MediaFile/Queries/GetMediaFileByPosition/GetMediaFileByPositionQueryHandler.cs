using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByPosition
{
    public class GetMediaFileByPositionQueryHandler : IRequestHandler<GetMediaFileByPositionQuery, HttpResult<MediaFileDto>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public GetMediaFileByPositionQueryHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<MediaFileDto>> Handle(GetMediaFileByPositionQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<MediaFileDto>> GetOnMongo(GetMediaFileByPositionQuery request)
        {
            var result = new HttpResult<MediaFileDto>();

            var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(request.AdId));
            if (!isExistsAdResult.Ok)
                return result.Fail(isExistsAdResult.StatusCode, isExistsAdResult.Message);

            var predicate = PredicateBuilder.True<MediaFileDocument>();
            predicate = predicate.And(f => f.AdId == request.AdId);
            predicate = predicate.And(f => f.Position == request.Position);
            predicate = predicate.And(f => f.EnabledFlag == true);

            var item = await MediaFileMongoDB.FirstOrDefaultAsync(filter: predicate);
            if (item == null)
                return result.Fail(404, $"MediaFile '{request.Position}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<MediaFileDto>();
            return result.Success(data);
        }
    }
}
