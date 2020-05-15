using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Banner.Queries.IsExistsBannerById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByBannerId
{
    public class GetMediaFileByBannerIdQueryHandler : IRequestHandler<GetMediaFileByBannerIdQuery, HttpResult<IEnumerable<MediaFileDto>>>
    {
        private IMediator Mediator { get; set; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public GetMediaFileByBannerIdQueryHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<IEnumerable<MediaFileDto>>> Handle(GetMediaFileByBannerIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.BannerId, request.Start, request.Length);
        }

        private async Task<HttpResult<IEnumerable<MediaFileDto>>> GetOnMongo(string bannerId, int start, int length)
        {
            var result = new HttpResult<IEnumerable<MediaFileDto>>();

            var isExistsCampaignResult = await Mediator.Send(new IsExistsBannerByIdQuery(bannerId));
            if (!isExistsCampaignResult.Ok)
                return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);

            var items = await MediaFileMongoDB.FindAsync(
                filter: f => f.BannerId == bannerId,
                sort: Builders<MediaFileDocument>.Sort.Descending(nameof(MediaFileDocument.Name)),
                start: start,
                length: length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<MediaFileDto>>();
            return result.Success(data);
        }
    }
}
