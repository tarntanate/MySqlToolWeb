using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserByName
{
    public class GetAdvertiserByNameQueryHandler : IRequestHandler<GetAdvertiserByNameQuery, HttpResult<AdvertiserDto>>
    {
        private AdsMongoRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public GetAdvertiserByNameQueryHandler(AdsMongoRepository<AdvertiserDocument> advertiserMongoDB)
        {
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<AdvertiserDto>> Handle(GetAdvertiserByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Name);
        }

        private async Task<HttpResult<AdvertiserDto>> GetOnMongo(string name)
        {
            var result = new HttpResult<AdvertiserDto>();
            var item = await AdvertiserMongoDB.FirstOrDefaultAsync(
                filter: f => f.Name == name && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"Advertiser '{name}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdvertiserDto>();
            return result.Success(data);
        }
    }
}
