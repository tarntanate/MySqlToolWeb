using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQueryHandler : IRequestHandler<GetAdListQuery, HttpResult<IEnumerable<AdDto>>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public GetAdListQueryHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<IEnumerable<AdDto>>> Handle(GetAdListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<AdDto>>> GetListMongoDB(GetAdListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdDto>>();

            var predicate = PredicateBuilder.True<AdDocument>();
            predicate = predicate.And(f => f.EnabledFlag == true);

            if (request.AdSlotId.HasValue())
                predicate = predicate.And(f => f.AdSlotId == request.AdSlotId);

            if (request.CampaignId.HasValue())
            {
                var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.CampaignId));
                if (!isExistsCampaignResult.Ok)
                    return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);
                predicate = predicate.And(f => f.CampaignId == request.CampaignId);
            }

            var items = await AdMongoDB.FindAsync(
                filter: predicate,
                sort: Builders<AdDocument>.Sort.Ascending(nameof(AdDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<AdDto>>();
            return result.Success(data);
        }
    }
}
