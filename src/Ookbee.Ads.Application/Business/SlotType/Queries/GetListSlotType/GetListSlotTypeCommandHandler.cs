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

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetListSlotType
{
    public class GetListSlotTypeCommandHandler : IRequestHandler<GetListSlotTypeCommand, HttpResult<IEnumerable<SlotTypeDto>>>
    {
        private OokbeeAdsMongoDBRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public GetListSlotTypeCommandHandler(OokbeeAdsMongoDBRepository<SlotTypeDocument> SlotTypeMongoRepo)
        {
            SlotTypeMongoDB = SlotTypeMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<SlotTypeDto>>> Handle(GetListSlotTypeCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<SlotTypeDto>>> GetListMongoDB(GetListSlotTypeCommand request)
        {
            var result = new HttpResult<IEnumerable<SlotTypeDto>>();
            var items = await SlotTypeMongoDB.FindAsync(
                sort: Builders<SlotTypeDocument>.Sort.Descending(nameof(SlotTypeDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<SlotTypeDto>>();
            return result.Success(data);
        }
    }
}
