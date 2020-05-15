using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByCampaingId
{
    public class GetBannerByCampaingIdQueryHandler : IRequestHandler<GetBannerByCampaingIdQuery, HttpResult<IEnumerable<BannerDto>>>
    {
        private IMediator Mediator { get; }
        private AdsMongoDBRepository<BannerDocument> BannerMongoDB { get; }

        public GetBannerByCampaingIdQueryHandler(
            IMediator mediator,
            AdsMongoDBRepository<BannerDocument> bannerMongoDB)
        {
            Mediator = mediator;
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<IEnumerable<BannerDto>>> Handle(GetBannerByCampaingIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.CampaingId, request.Start, request.Length);
        }

        private async Task<HttpResult<IEnumerable<BannerDto>>> GetOnMongo(string campaignId, int start, int length)
        {
            var result = new HttpResult<IEnumerable<BannerDto>>();

            var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(campaignId));
            if (!isExistsCampaignResult.Ok)
                return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);

            var items = await BannerMongoDB.FindAsync(
                filter: f => f.CampaignId == campaignId,
                sort: Builders<BannerDocument>.Sort.Descending(nameof(BannerDocument.Name)),
                start: start,
                length: length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<BannerDto>>();
            return result.Success(data);
        }
    }
}
