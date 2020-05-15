using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotById;
using Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotByName;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById;
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
            var document = Mapper.Map(request).ToANew<AdSlotDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(AdSlotDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var publisherResult = await Mediator.Send(new GetPublisherByIdQuery(document.PublisherId));
                if (!publisherResult.Ok)
                    return result.Fail(publisherResult.StatusCode, publisherResult.Message);
                    
                var isExistsResult = await Mediator.Send(new IsExistsAdSlotByIdQuery(document.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var isExistsAdSlotByNameResult = await Mediator.Send(new IsExistsAdSlotByNameQuery(document.Name));
                if (isExistsAdSlotByNameResult.Data)
                    return result.Fail(409, $"AdSlot '{document.Name}' already exists.");

                var now = MechineDateTime.Now;
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
