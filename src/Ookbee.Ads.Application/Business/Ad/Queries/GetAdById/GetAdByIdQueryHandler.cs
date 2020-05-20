using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdById
{
    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, HttpResult<AdDto>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public GetAdByIdQueryHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<AdDto>> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<AdDto>> GetOnMongo(GetAdByIdQuery request)
        {
            var result = new HttpResult<AdDto>();

            var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.CampaignId));
            if (!isExistsCampaignResult.Ok)
                return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);

            var item = await AdMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == request.Id && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"Ad '{request.Id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdDto>();
            return result.Success(data);
        }
    }
}
