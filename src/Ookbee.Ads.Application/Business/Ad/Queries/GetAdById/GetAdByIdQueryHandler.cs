using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdById
{
    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, HttpResult<AdDto>>
    {
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public GetAdByIdQueryHandler(AdsMongoRepository<AdDocument> adMongoDB)
        {
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<AdDto>> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<AdDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<AdDto>();
            var item = await AdMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"Ad '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdDto>();
            return result.Success(data);
        }
    }
}
