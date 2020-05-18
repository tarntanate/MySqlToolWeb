using AgileObjects.AgileMapper;
using MediatR;
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

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.CreateAdSlot
{
    public class CreateAdSlotCommandHandler : IRequestHandler<CreateAdSlotCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdSlotDocument> AdSlotMongoDB { get; }

        public CreateAdSlotCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdSlotDocument> adSlotMongoDB)
        {
            Mediator = mediator;
            AdSlotMongoDB = adSlotMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateAdSlotCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateAdSlotCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
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
                await AdSlotMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
