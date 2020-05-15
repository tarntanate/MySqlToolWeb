using MediatR;
using Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.DeleteAdSlot
{
    public class DeleteAdSlotCommandHandler : IRequestHandler<DeleteAdSlotCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdSlotMongoDB { get; }

        public DeleteAdSlotCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adSlotMongoDB)
        {
            Mediator = mediator;
            AdSlotMongoDB = adSlotMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdSlotCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsAdSlotByIdQuery(id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await AdSlotMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
