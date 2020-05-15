using MediatR;
using Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.DeleteSlotType
{
    public class DeleteSlotTypeCommandHandler : IRequestHandler<DeleteSlotTypeCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoDBRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public DeleteSlotTypeCommandHandler(
            IMediator mediator,
            AdsMongoDBRepository<SlotTypeDocument> slotTypeMongoDB)
        {
            Mediator = mediator;
            SlotTypeMongoDB = slotTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteSlotTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsSlotTypeByIdQuery(id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await SlotTypeMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
