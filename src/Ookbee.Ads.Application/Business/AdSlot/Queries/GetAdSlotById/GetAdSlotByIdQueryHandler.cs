using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById
{
    public class GetAdSlotByIdQueryHandler : IRequestHandler<GetAdSlotByIdQuery, HttpResult<AdSlotDto>>
    {
        private AdsMongoRepository<AdSlotDocument> AdSlotMongoDB { get; }

        public GetAdSlotByIdQueryHandler(AdsMongoRepository<AdSlotDocument> adSlotMongoDB)
        {
            AdSlotMongoDB = adSlotMongoDB;
        }

        public async Task<HttpResult<AdSlotDto>> Handle(GetAdSlotByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<AdSlotDto>> GetOnMongo(GetAdSlotByIdQuery request)
        {
            var result = new HttpResult<AdSlotDto>();
            var item = await AdSlotMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == request.Id &&
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"AdSlot '{request.Id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdSlotDto>();
            return result.Success(data);
        }
    }
}
