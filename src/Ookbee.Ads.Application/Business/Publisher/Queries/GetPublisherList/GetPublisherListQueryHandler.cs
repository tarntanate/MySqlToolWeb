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

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherList
{
    public class GetPublisherListQueryHandler : IRequestHandler<GetPublisherListQuery, HttpResult<IEnumerable<PublisherDto>>>
    {
        private AdsMongoRepository<PublisherDocument> PublisherMongoDB { get; }

        public GetPublisherListQueryHandler(AdsMongoRepository<PublisherDocument> publisherMongoDB)
        {
            PublisherMongoDB = publisherMongoDB;
        }

        public async Task<HttpResult<IEnumerable<PublisherDto>>> Handle(GetPublisherListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<PublisherDto>>> GetListMongoDB(GetPublisherListQuery request)
        {
            var result = new HttpResult<IEnumerable<PublisherDto>>();
            var items = await PublisherMongoDB.FindAsync(
                filter: f => f.EnabledFlag == true,
                sort: Builders<PublisherDocument>.Sort.Ascending(nameof(PublisherDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<PublisherDto>>();
            return result.Success(data);
        }
    }
}
