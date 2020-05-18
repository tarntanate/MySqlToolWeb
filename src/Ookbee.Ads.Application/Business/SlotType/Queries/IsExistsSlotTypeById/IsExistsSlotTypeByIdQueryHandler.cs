﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeById
{
    public class IsExistsSlotTypeByIdQueryHandler : IRequestHandler<IsExistsSlotTypeByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public IsExistsSlotTypeByIdQueryHandler(AdsMongoRepository<SlotTypeDocument> slotTypeMongoDB)
        {
            SlotTypeMongoDB = slotTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsSlotTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await SlotTypeMongoDB.AnyAsync(
                filter: f => f.Id == id && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"SlotType '{id}' doesn't exist.");
        }
    }
}