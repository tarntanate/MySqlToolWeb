using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeByName;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.CreateSlotType
{
    public class CreateSlotTypeCommandHandler : IRequestHandler<CreateSlotTypeCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<SlotTypeDocument> SlotTypeMongoDB { get; }

        public CreateSlotTypeCommandHandler(
            IMediator mediator,
            AdsMongoRepository<SlotTypeDocument> slotTypeMongoDB)
        {
            Mediator = mediator;
            SlotTypeMongoDB = slotTypeMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateSlotTypeCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<SlotTypeDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(SlotTypeDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediator.Send(new IsExistsSlotTypeByNameQuery(document.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"SlotType '{document.Name}' already exists.");

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                document.EnabledFlag = true;
                await SlotTypeMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

    }
}
