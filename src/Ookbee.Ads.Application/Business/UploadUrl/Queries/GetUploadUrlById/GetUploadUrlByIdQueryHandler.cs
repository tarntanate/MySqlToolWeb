using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.SlotType;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrlById
{
    public class GetUploadUrlByIdQueryHandler : IRequestHandler<GetUploadUrlByIdQuery, HttpResult<UploadUrlDto>>
    {
        private OokbeeAdsMongoDBRepository<UploadUrlDocument> UploadUrlMongoDB { get; }

        public GetUploadUrlByIdQueryHandler(OokbeeAdsMongoDBRepository<UploadUrlDocument> uploadUrlMongoDB)
        {
            UploadUrlMongoDB = uploadUrlMongoDB;
        }

        public async Task<HttpResult<UploadUrlDto>> Handle(GetUploadUrlByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<UploadUrlDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<UploadUrlDto>();
            var item = await UploadUrlMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"UploadUrl '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<UploadUrlDto>();
            return result.Success(data);
        }
    }
}
