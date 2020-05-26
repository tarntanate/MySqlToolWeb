using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeByName;
using Ookbee.Ads.Common;
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
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateSlotTypeCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediator.Send(new IsExistsSlotTypeByNameQuery(request.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"SlotType '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<SlotTypeDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
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
