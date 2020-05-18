using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileById
{
    public class GetMediaFileByIdQueryHandler : IRequestHandler<GetMediaFileByIdQuery, HttpResult<MediaFileDto>>
    {
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public GetMediaFileByIdQueryHandler(AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<MediaFileDto>> Handle(GetMediaFileByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<MediaFileDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<MediaFileDto>();
            var item = await MediaFileMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == id && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"MediaFile '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<MediaFileDto>();
            return result.Success(data);
        }
    }
}
