using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotByName
{
    public class GetAdSlotByNameQueryHandler : IRequestHandler<GetAdSlotByNameQuery, HttpResult<AdSlotDto>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdSlotDocument> AdSlotMongoDB { get; }

        public GetAdSlotByNameQueryHandler(
            IMediator mediator,
            AdsMongoRepository<AdSlotDocument> adSlotMongoDB)
        {
            Mediator = mediator;
            AdSlotMongoDB = adSlotMongoDB;
        }

        public async Task<HttpResult<AdSlotDto>> Handle(GetAdSlotByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<AdSlotDto>> GetOnMongo(GetAdSlotByNameQuery request)
        {
            var result = new HttpResult<AdSlotDto>();

            var isExistsPublisherResult = await Mediator.Send(new IsExistsPublisherByIdQuery(request.PublisherId));
            if (!isExistsPublisherResult.Ok)
                return result.Fail(isExistsPublisherResult.StatusCode, isExistsPublisherResult.Message);

            var item = await AdSlotMongoDB.FirstOrDefaultAsync(
                filter: f => f.PublisherId == request.PublisherId &&
                             f.Name == request.Name &&
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"AdSlot '{request.Name}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdSlotDto>();
            return result.Success(data);
        }
    }
}
