using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileList
{
    public class GetMediaFileListQueryHandler : IRequestHandler<GetMediaFileListQuery, HttpResult<IEnumerable<MediaFileDto>>>
    {
        private OokbeeAdsMongoDBRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public GetMediaFileListQueryHandler(OokbeeAdsMongoDBRepository<MediaFileDocument> mediaFileMongoDB)
        {
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<IEnumerable<MediaFileDto>>> Handle(GetMediaFileListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<MediaFileDto>>> GetListMongoDB(GetMediaFileListQuery request)
        {
            var result = new HttpResult<IEnumerable<MediaFileDto>>();
            var items = await MediaFileMongoDB.FindAsync(
                sort: Builders<MediaFileDocument>.Sort.Descending(nameof(MediaFileDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<MediaFileDto>>();
            return result.Success(data);
        }
    }
}
