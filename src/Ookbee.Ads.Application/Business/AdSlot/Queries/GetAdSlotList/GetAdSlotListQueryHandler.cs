using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotList
{
    public class GetAdSlotListQueryHandler : IRequestHandler<GetAdSlotListQuery, HttpResult<IEnumerable<AdSlotDto>>>
    {
        private AdsMongoRepository<AdSlotDocument> AdMongoDB { get; }

        public GetAdSlotListQueryHandler(AdsMongoRepository<AdSlotDocument> adMongoDB)
        {
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<IEnumerable<AdSlotDto>>> Handle(GetAdSlotListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<AdSlotDto>>> GetListMongoDB(GetAdSlotListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdSlotDto>>();
            var items = await AdMongoDB.FindAsync(
                filter: f => f.PublisherId == request.PublisherId &&
                             f.EnabledFlag == true,
                sort: Builders<AdSlotDocument>.Sort.Ascending(nameof(AdDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<AdSlotDto>>();
            return result.Success(data);
        }
    }
}
