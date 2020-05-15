using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById
{
    public class IsExistsMediaFileByIdQueryHandler : IRequestHandler<IsExistsMediaFileByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public IsExistsMediaFileByIdQueryHandler(AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsMediaFileByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await MediaFileMongoDB.AnyAsync(filter: f => f.Id == id);
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"MediaFile '{id}' doesn't exist.");
        }
    }
}
