using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileList
{
    public class GetMediaFileListQueryHandler : IRequestHandler<GetMediaFileListQuery, HttpResult<IEnumerable<MediaFileDto>>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public GetMediaFileListQueryHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }


        public async Task<HttpResult<IEnumerable<MediaFileDto>>> Handle(GetMediaFileListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<MediaFileDto>>> GetListMongoDB(GetMediaFileListQuery request)
        {
            var result = new HttpResult<IEnumerable<MediaFileDto>>();

            var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(request.CampaignId, request.AdId));
            if (!isExistsAdResult.Ok)
                return result.Fail(isExistsAdResult.StatusCode, isExistsAdResult.Message);

            var items = await MediaFileMongoDB.FindAsync(
                filter: f => f.AdId == request.AdId &&
                             f.EnabledFlag == true,
                sort: Builders<MediaFileDocument>.Sort.Ascending(nameof(MediaFileDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<MediaFileDto>>();
            return result.Success(data);
        }
    }
}
