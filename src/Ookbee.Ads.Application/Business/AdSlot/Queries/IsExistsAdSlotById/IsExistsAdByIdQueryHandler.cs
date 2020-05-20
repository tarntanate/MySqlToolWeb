﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotById
{
    public class IsExistsAdSlotByIdQueryHandler : IRequestHandler<IsExistsAdSlotByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<AdSlotDocument> AdMongoDB { get; }

        public IsExistsAdSlotByIdQueryHandler(AdsMongoRepository<AdSlotDocument> adMongoDB)
        {
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdSlotByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(IsExistsAdSlotByIdQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdMongoDB.AnyAsync(
                filter: f => f.Id == request.Id && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"AdSlot '{request.Id}' doesn't exist.");
        }
    }
}
