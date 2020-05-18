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
            return await IsExistsByNameOnMongo(request.Name);
        }

        private async Task<HttpResult<bool>> IsExistsByNameOnMongo(string name)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdSlotMongoDB.AnyAsync(
                filter: f => f.Name == name && 
                             f.EnabledFlag == true
                );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"AdSlot '{name}' doesn't exist.");
        }
    }
}
