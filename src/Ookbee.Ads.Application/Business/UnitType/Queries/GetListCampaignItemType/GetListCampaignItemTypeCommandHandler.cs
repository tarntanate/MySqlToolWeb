using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UnitType.Queries.GetListUnitType
{
    public class GetListUnitTypeCommandHandler : IRequestHandler<GetListUnitTypeCommand, HttpResult<IEnumerable<UnitTypeDto>>>
    {
        private OokbeeAdsMongoDBRepository<UnitTypeDocument> UnitTypeMongoDB { get; }

        public GetListUnitTypeCommandHandler(OokbeeAdsMongoDBRepository<UnitTypeDocument> UnitTypeMongoRepo)
        {
            UnitTypeMongoDB = UnitTypeMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<UnitTypeDto>>> Handle(GetListUnitTypeCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<UnitTypeDto>>> GetListMongoDB(GetListUnitTypeCommand request)
        {
            var result = new HttpResult<IEnumerable<UnitTypeDto>>();
            var items = await UnitTypeMongoDB.FindAsync(
                sort: Builders<UnitTypeDocument>.Sort.Descending(nameof(UnitTypeDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<UnitTypeDto>>();
            return result.Success(data);
        }
    }
}
