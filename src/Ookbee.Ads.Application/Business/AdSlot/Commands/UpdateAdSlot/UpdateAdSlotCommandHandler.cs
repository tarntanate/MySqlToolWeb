using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotById;
using Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotByName;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById;
using Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeById;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.UpdateAdSlot
{
    public class UpdateAdSlotCommandHandler : IRequestHandler<UpdateAdSlotCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdSlotDocument> AdSlotMongoDB { get; }

        public UpdateAdSlotCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdSlotDocument> adSlotMongoDB)
        {
            Mediator = mediator;
            AdSlotMongoDB = adSlotMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdSlotCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateAdSlotCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsAdSlotByIdResult = await Mediator.Send(new IsExistsAdSlotByIdQuery(request.Id));
                if (!isExistsAdSlotByIdResult.Ok)
                    return isExistsAdSlotByIdResult;
                    
                var publisherResult = await Mediator.Send(new GetPublisherByIdQuery(request.PublisherId));
                if (!publisherResult.Ok)
                    return result.Fail(publisherResult.StatusCode, publisherResult.Message);

                var slotTypeResult = await Mediator.Send(new GetSlotTypeByIdQuery(request.SlotTypeId));
                if (!slotTypeResult.Ok)
                    return result.Fail(slotTypeResult.StatusCode, slotTypeResult.Message);

                var isExistsAdSlotByNameResult = await Mediator.Send(new IsExistsAdSlotByNameQuery(request.Name));
                if (isExistsAdSlotByNameResult.Data)
                    return result.Fail(409, $"AdSlot '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<AdSlotDocument>();
                document.Publisher = Mapper.Map(publisherResult.Data).ToANew<DefaultDocument>();
                document.SlotType = Mapper.Map(slotTypeResult.Data).ToANew<DefaultDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await AdSlotMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
