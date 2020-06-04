using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeById;
using Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeByName;
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
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateSlotTypeCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var slotTypeResult = await Mediator.Send(new GetSlotTypeByIdQuery(request.Id));
                if (!slotTypeResult.Ok)
                    return result.Fail(slotTypeResult.StatusCode, slotTypeResult.Message);

                var slotTypeByNameResult = await Mediator.Send(new GetSlotTypeByNameQuery(request.Name));
                if (slotTypeByNameResult.Ok &&
                    slotTypeByNameResult.Data.Id != request.Id &&
                    slotTypeByNameResult.Data.Name == request.Name)
                    return result.Fail(409, $"SlotType '{request.Name}' already exists.");

                var template = Mapper.Map(request).Over(slotTypeResult.Data);
                var document = Mapper.Map(template).ToANew<SlotTypeDocument>();
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
