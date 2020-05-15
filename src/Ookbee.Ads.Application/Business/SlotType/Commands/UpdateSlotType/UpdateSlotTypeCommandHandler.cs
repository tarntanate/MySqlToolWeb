using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeById;
using Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeByName;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.UpdateSlotType
{
    public class UpdateSlotTypeCommandHandler : IRequestHandler<UpdateSlotTypeCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public UpdateSlotTypeCommandHandler(
            IMediator mediator,
            AdsMongoRepository<SlotTypeDocument> slotTypeMongoDB)
        {
            Mediator = mediator;
            SlotTypeMongoDB = slotTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateSlotTypeCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<SlotTypeDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(SlotTypeDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsByIdResult = await Mediator.Send(new IsExistsSlotTypeByIdQuery(document.Id));
                if (!isExistsByIdResult.Ok)
                    return isExistsByIdResult;
                
                var isExistsByNameResult = await Mediator.Send(new IsExistsSlotTypeByNameQuery(document.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"SlotType '{document.Name}' already exists.");

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await SlotTypeMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
