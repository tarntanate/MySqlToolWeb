using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotByName
{
    public class IsExistsAdSlotByNameQueryHandler : IRequestHandler<IsExistsAdSlotByNameQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<AdSlotDocument> AdSlotMongoDB { get; }

        public IsExistsAdSlotByNameQueryHandler(AdsMongoRepository<AdSlotDocument> adSlotMongoDB)
        {
            AdSlotMongoDB = adSlotMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdSlotByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByNameOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByNameOnMongo(IsExistsAdSlotByNameQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdSlotMongoDB.AnyAsync(
                filter: f => f.Name == request.Name && 
                             f.DeletedAt == null
                );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"AdSlot '{request.Name}' doesn't exist.");
        }
    }
}
