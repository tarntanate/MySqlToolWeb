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

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByName
{
    public class GetMediaFileByNameQueryHandler : IRequestHandler<GetMediaFileByNameQuery, HttpResult<MediaFileDto>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public GetMediaFileByNameQueryHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<MediaFileDto>> Handle(GetMediaFileByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<MediaFileDto>> GetOnMongo(GetMediaFileByNameQuery request)
        {
            var result = new HttpResult<MediaFileDto>();

            var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(request.AdId));
            if (!isExistsAdResult.Ok)
                return result.Fail(isExistsAdResult.StatusCode, isExistsAdResult.Message);

            var predicate = PredicateBuilder.True<MediaFileDocument>();
            predicate = predicate.And(f => f.Name == request.Name);
            predicate = predicate.And(f => f.EnabledFlag == true);

            if (request.AdId.HasValue())
                predicate = predicate.And(f => f.AdId == request.AdId);

            var item = await MediaFileMongoDB.FirstOrDefaultAsync(filter: predicate);
            if (item == null)
                return result.Fail(404, $"MediaFile '{request.Name}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<MediaFileDto>();
            return result.Success(data);
        }
    }
}
