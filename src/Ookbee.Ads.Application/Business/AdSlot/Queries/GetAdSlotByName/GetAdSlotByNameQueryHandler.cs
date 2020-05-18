using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotByName
{
    public class GetAdSlotByNameQueryHandler : IRequestHandler<GetAdSlotByNameQuery, HttpResult<AdSlotDto>>
    {
        private AdsMongoRepository<AdSlotDocument> AdSlotMongoDB { get; }

        public GetAdSlotByNameQueryHandler(AdsMongoRepository<AdSlotDocument> adSlotMongoDB)
        {
            AdSlotMongoDB = adSlotMongoDB;
        }

        public async Task<HttpResult<AdSlotDto>> Handle(GetAdSlotByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Name);
        }

        private async Task<HttpResult<AdSlotDto>> GetOnMongo(string name)
        {
            var result = new HttpResult<AdSlotDto>();
            var item = await AdSlotMongoDB.FirstOrDefaultAsync(
                filter: f => f.Name == name &&
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"AdSlot '{name}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdSlotDto>();
            return result.Success(data);
        }
    }
}
