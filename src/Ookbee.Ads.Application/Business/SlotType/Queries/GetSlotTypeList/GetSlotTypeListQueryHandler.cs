using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeList
{
    public class GetSlotTypeListQueryHandler : IRequestHandler<GetSlotTypeListQuery, HttpResult<IEnumerable<SlotTypeDto>>>
    {
        private AdsMongoRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public GetSlotTypeListQueryHandler(AdsMongoRepository<SlotTypeDocument> SlotTypeMongoRepo)
        {
            SlotTypeMongoDB = SlotTypeMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<SlotTypeDto>>> Handle(GetSlotTypeListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<SlotTypeDto>>> GetListMongoDB(GetSlotTypeListQuery request)
        {
            var result = new HttpResult<IEnumerable<SlotTypeDto>>();
            var items = await SlotTypeMongoDB.FindAsync(
                filter: f => f.EnabledFlag == true,
                sort: Builders<SlotTypeDocument>.Sort.Descending(nameof(SlotTypeDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<SlotTypeDto>>();
            return result.Success(data);
        }
    }
}
