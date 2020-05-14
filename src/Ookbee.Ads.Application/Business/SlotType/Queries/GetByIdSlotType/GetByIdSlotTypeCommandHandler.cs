using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetByIdSlotType
{
    public class GetByIdSlotTypeCommandHandler : IRequestHandler<GetByIdSlotTypeCommand, HttpResult<SlotTypeDto>>
    {
        private OokbeeAdsMongoDBRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public GetByIdSlotTypeCommandHandler(OokbeeAdsMongoDBRepository<SlotTypeDocument> slotTypeMongoDB)
        {
            SlotTypeMongoDB = slotTypeMongoDB;
        }

        public async Task<HttpResult<SlotTypeDto>> Handle(GetByIdSlotTypeCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<SlotTypeDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<SlotTypeDto>();
            var item = await SlotTypeMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"SlotType '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<SlotTypeDto>();
            return result.Success(data);
        }
    }
}
