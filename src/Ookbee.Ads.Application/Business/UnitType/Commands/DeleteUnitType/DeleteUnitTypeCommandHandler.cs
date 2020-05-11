using MediatR;
using Ookbee.Ads.Application.Business.UnitType.Queries.IsExistsByIdUnitType;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UnitType.Commands.DeleteUnitType
{
    public class DeleteUnitTypeCommandHandler : IRequestHandler<DeleteUnitTypeCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<UnitTypeDocument> UnitTypeMongoDB { get; }

        public DeleteUnitTypeCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<UnitTypeDocument> unitTypeMongoDB)
        {
            Mediatr = mediatr;
            UnitTypeMongoDB = unitTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdUnitTypeCommand(id));
            if (!isExistsByNameResult.Data)
                return result.Fail(404, $"ItemType '{id}' doesn't exist.");

            await UnitTypeMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
