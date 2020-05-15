using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeByName
{
    public class IsExistsSlotTypeByNameQueryHandler : IRequestHandler<IsExistsSlotTypeByNameQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public IsExistsSlotTypeByNameQueryHandler(AdsMongoRepository<SlotTypeDocument> slotTypeMongoDB)
        {
            SlotTypeMongoDB = slotTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsSlotTypeByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByNameOnMongo(request.Name);
        }

        private async Task<HttpResult<bool>> IsExistsByNameOnMongo(string name)
        {
            var result = new HttpResult<bool>();
            var isExists = await SlotTypeMongoDB.AnyAsync(filter: f => f.Name == name);
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"SlotType '{name}' doesn't exist.");
        }
    }
}
