using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById;
using Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotByName;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeById;
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
                var adSlotResult = await Mediator.Send(new GetAdSlotByIdQuery(request.Id));
                if (!adSlotResult.Ok)
                    return result.Fail(adSlotResult.StatusCode, adSlotResult.Message);

                var isExistsPublisherResult = await Mediator.Send(new IsExistsPublisherByIdQuery(request.PublisherId));
                if (!isExistsPublisherResult.Ok)
                    return result.Fail(isExistsPublisherResult.StatusCode, isExistsPublisherResult.Message);

                var isExistsSlotTypeResult = await Mediator.Send(new IsExistsSlotTypeByIdQuery(request.SlotTypeId));
                if (!isExistsSlotTypeResult.Ok)
                    return result.Fail(isExistsSlotTypeResult.StatusCode, isExistsSlotTypeResult.Message);

                var isExistsAdSlotByNameResult = await Mediator.Send(new IsExistsAdSlotByNameQuery(request.Name));
                if (isExistsAdSlotByNameResult.Data)
                    return result.Fail(409, $"AdSlot '{request.Name}' already exists.");

                var template = Mapper.Map(request).Over(adSlotResult.Data);
                var document = Mapper.Map(template).ToANew<AdSlotDocument>();
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
