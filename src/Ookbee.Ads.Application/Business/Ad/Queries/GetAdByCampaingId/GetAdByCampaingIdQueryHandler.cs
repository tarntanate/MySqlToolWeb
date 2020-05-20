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

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaignId
{
    public class GetAdByCampaignIdQueryHandler : IRequestHandler<GetAdByCampaignIdQuery, HttpResult<IEnumerable<AdDto>>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public GetAdByCampaignIdQueryHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<IEnumerable<AdDto>>> Handle(GetAdByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<IEnumerable<AdDto>>> GetOnMongo(GetAdByCampaignIdQuery request)
        {
            var result = new HttpResult<IEnumerable<AdDto>>();

            var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.CampaignId));
            if (!isExistsCampaignResult.Ok)
                return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);

            var items = await AdMongoDB.FindAsync(
                filter: f => f.Campaign.Id == request.CampaignId && 
                             f.EnabledFlag == true,
                sort: Builders<AdDocument>.Sort.Ascending(nameof(AdDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<AdDto>>();
            return result.Success(data);
        }
    }
}
