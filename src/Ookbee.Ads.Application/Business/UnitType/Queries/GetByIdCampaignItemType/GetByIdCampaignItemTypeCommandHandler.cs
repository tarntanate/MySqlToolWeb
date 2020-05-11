using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UnitType.Queries.GetByIdUnitType
{
    public class GetByIdUnitTypeCommandHandler : IRequestHandler<GetByIdUnitTypeCommand, HttpResult<UnitTypeDto>>
    {
        private OokbeeAdsMongoDBRepository<UnitTypeDocument> UnitTypeMongoDB { get; }

        public GetByIdUnitTypeCommandHandler(OokbeeAdsMongoDBRepository<UnitTypeDocument> unitTypeMongoDB)
        {
            UnitTypeMongoDB = unitTypeMongoDB;
        }

        public async Task<HttpResult<UnitTypeDto>> Handle(GetByIdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<UnitTypeDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<UnitTypeDto>();
            var item = await UnitTypeMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"ItemType '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<UnitTypeDto>();
            return result.Success(data);
        }
    }
}
