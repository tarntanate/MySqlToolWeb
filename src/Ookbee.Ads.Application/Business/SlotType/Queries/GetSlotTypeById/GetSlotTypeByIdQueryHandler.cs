﻿using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeById
{
    public class GetSlotTypeByIdQueryHandler : IRequestHandler<GetSlotTypeByIdQuery, HttpResult<SlotTypeDto>>
    {
        private AdsMongoRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public GetSlotTypeByIdQueryHandler(AdsMongoRepository<SlotTypeDocument> slotTypeMongoDB)
        {
            SlotTypeMongoDB = slotTypeMongoDB;
        }

        public async Task<HttpResult<SlotTypeDto>> Handle(GetSlotTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<SlotTypeDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<SlotTypeDto>();
            var item = await SlotTypeMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == id && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"SlotType '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<SlotTypeDto>();
            return result.Success(data);
        }
    }
}