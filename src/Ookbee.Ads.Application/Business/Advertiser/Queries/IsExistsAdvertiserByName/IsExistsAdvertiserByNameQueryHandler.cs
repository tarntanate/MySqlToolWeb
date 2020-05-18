﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName
{
    public class IsExistsAdvertiserByNameQueryHandler : IRequestHandler<IsExistsAdvertiserByNameQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public IsExistsAdvertiserByNameQueryHandler(AdsMongoRepository<AdvertiserDocument> advertiserMongoDB)
        {
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdvertiserByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByNameOnMongo(request.Name);
        }

        private async Task<HttpResult<bool>> IsExistsByNameOnMongo(string name)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdvertiserMongoDB.AnyAsync(
                filter: f => f.Name == name && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Advertiser '{name}' doesn't exist.");
        }
    }
}