using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetByIdMediaFile
{
    public class GetByIdMediaFileCommandHandler : IRequestHandler<GetByIdMediaFileCommand, HttpResult<MediaFileDto>>
    {
        private OokbeeAdsMongoDBRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public GetByIdMediaFileCommandHandler(OokbeeAdsMongoDBRepository<MediaFileDocument> mediaFileMongoDB)
        {
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<MediaFileDto>> Handle(GetByIdMediaFileCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<MediaFileDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<MediaFileDto>();
            var item = await MediaFileMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"Advertiser '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<MediaFileDto>();
            return result.Success(data);
        }
    }
}
