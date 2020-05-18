using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaingId
{
    public class GetAdByCampaingIdQueryHandler : IRequestHandler<GetAdByCampaingIdQuery, HttpResult<IEnumerable<AdDto>>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public GetAdByCampaingIdQueryHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<IEnumerable<AdDto>>> Handle(GetAdByCampaingIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.CampaingId, request.Start, request.Length);
        }

        private async Task<HttpResult<IEnumerable<AdDto>>> GetOnMongo(string campaignId, int start, int length)
        {
            var result = new HttpResult<IEnumerable<AdDto>>();

            var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(campaignId));
            if (!isExistsCampaignResult.Ok)
                return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);

            var items = await AdMongoDB.FindAsync(
                filter: f => f.CampaignId == campaignId && 
                             f.EnabledFlag == true,
                sort: Builders<AdDocument>.Sort.Descending(nameof(AdDocument.Name)),
                start: start,
                length: length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<AdDto>>();
            return result.Success(data);
        }
    }
}
