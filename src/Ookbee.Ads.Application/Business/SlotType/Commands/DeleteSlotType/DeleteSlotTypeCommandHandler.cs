using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.DeleteSlotType
{
    public class DeleteSlotTypeCommandHandler : IRequestHandler<DeleteSlotTypeCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public DeleteSlotTypeCommandHandler(
            IMediator mediator,
            AdsMongoRepository<SlotTypeDocument> slotTypeMongoDB)
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

            await SlotTypeMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == id, 
                update: Builders<SlotTypeDocument>.Update.Set(f => f.DeletedAt, MechineDateTime.Now.DateTime)
            );
            return result.Success(true);
        }
    }
}
