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

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserList
{
    public class GetAdvertiserListQueryHandler : IRequestHandler<GetAdvertiserListQuery, HttpResult<IEnumerable<AdvertiserDto>>>
    {
        private AdsMongoDBRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public GetAdvertiserListQueryHandler(AdsMongoDBRepository<AdvertiserDocument> advertiserMongoDB)
        {
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<IEnumerable<AdvertiserDto>>> Handle(GetAdvertiserListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<AdvertiserDto>>> GetListMongoDB(GetAdvertiserListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdvertiserDto>>();
            var items = await AdvertiserMongoDB.FindAsync(
                sort: Builders<AdvertiserDocument>.Sort.Descending(nameof(AdvertiserDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<AdvertiserDto>>();
            return result.Success(data);
        }
    }
}
