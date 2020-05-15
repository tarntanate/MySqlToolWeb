using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetByBannerIdMediaFile
{
    public class GetByBannerIdMediaFileCommandHandler : IRequestHandler<GetByBannerIdMediaFileCommand, HttpResult<IEnumerable<MediaFileDto>>>
    {
        private OokbeeAdsMongoDBRepository<BannerDocument> MediaFileMongoDB { get; }

        public GetByBannerIdMediaFileCommandHandler(OokbeeAdsMongoDBRepository<BannerDocument> mediaFileMongoDB)
        {
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<IEnumerable<MediaFileDto>>> Handle(GetByBannerIdMediaFileCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.BannerId, request.Start, request.Length);
        }

        private async Task<HttpResult<IEnumerable<MediaFileDto>>> GetOnMongo(string bannerId, int start, int length)
        {
            var result = new HttpResult<IEnumerable<MediaFileDto>>();
            var items = await MediaFileMongoDB.FindAsync(
                sort: Builders<BannerDocument>.Sort.Descending(nameof(BannerDocument.Name)),
                start: start,
                length: length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<MediaFileDto>>();
            return result.Success(data);
        }
    }
}
