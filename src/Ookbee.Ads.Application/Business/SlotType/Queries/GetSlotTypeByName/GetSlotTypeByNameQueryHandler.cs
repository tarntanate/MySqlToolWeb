using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeByName
{
    public class GetSlotTypeByNameQueryHandler : IRequestHandler<GetSlotTypeByNameQuery, HttpResult<SlotTypeDto>>
    {
        private AdsMongoRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public GetSlotTypeByNameQueryHandler(AdsMongoRepository<SlotTypeDocument> slotTypeMongoDB)
        {
            SlotTypeMongoDB = slotTypeMongoDB;
        }

        public async Task<HttpResult<SlotTypeDto>> Handle(GetSlotTypeByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Name);
        }

        private async Task<HttpResult<SlotTypeDto>> GetOnMongo(string name)
        {
            var result = new HttpResult<SlotTypeDto>();
            var item = await SlotTypeMongoDB.FirstOrDefaultAsync(
                filter: f => f.Name == name && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"SlotType '{name}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<SlotTypeDto>();
            return result.Success(data);
        }
    }
}
