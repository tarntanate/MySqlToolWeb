using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UnitType.Queries.IsExistsByIdUnitType
{
    public class IsExistsByIdUnitTypeCommandHandler : IRequestHandler<IsExistsByIdUnitTypeCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<UnitTypeDocument> UnitTypeMongoDB { get; }

        public IsExistsByIdUnitTypeCommandHandler(OokbeeAdsMongoDBRepository<UnitTypeDocument> unitTypeMongoDB)
        {
            UnitTypeMongoDB = unitTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByIdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await UnitTypeMongoDB.AnyAsync(filter: f => f.Id == id);
            return result.Success(isExists);
        }
    }
}
