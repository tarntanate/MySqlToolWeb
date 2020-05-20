using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById
{
    public class GetAdvertiserByIdQueryHandler : IRequestHandler<GetAdvertiserByIdQuery, HttpResult<AdvertiserDto>>
    {
        private AdsMongoRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public GetAdvertiserByIdQueryHandler(AdsMongoRepository<AdvertiserDocument> advertiserMongoDB)
        {
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<AdvertiserDto>> Handle(GetAdvertiserByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<AdvertiserDto>> GetOnMongo(GetAdvertiserByIdQuery request)
        {
            var result = new HttpResult<AdvertiserDto>();
            var item = await AdvertiserMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == request.Id && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"Advertiser '{request.Id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdvertiserDto>();
            return result.Success(data);
        }
    }
}
