using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByName
{
    public class GetAdByNameQueryHandler : IRequestHandler<GetAdByNameQuery, HttpResult<AdDto>>
    {
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public GetAdByNameQueryHandler(AdsMongoRepository<AdDocument> adMongoDB)
        {
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<AdDto>> Handle(GetAdByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<AdDto>> GetOnMongo(GetAdByNameQuery request)
        {
            var result = new HttpResult<AdDto>();
            var item = await AdMongoDB.FirstOrDefaultAsync(
                filter: f => f.Name == request.Name && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"Ad '{request.Name}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdDto>();
            return result.Success(data);
        }
    }
}
