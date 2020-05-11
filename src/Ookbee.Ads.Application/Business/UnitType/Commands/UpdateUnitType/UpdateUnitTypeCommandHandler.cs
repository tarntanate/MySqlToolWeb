using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UnitType.Commands.UpdateUnitType
{
    public class UpdateUnitTypeCommandHandler : IRequestHandler<UpdateUnitTypeCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<UnitTypeDocument> UnitTypeMongoDB { get; }

        public UpdateUnitTypeCommandHandler(OokbeeAdsMongoDBRepository<UnitTypeDocument> unitTypeMongoDB)
        {
            UnitTypeMongoDB = unitTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<UnitTypeDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UnitTypeDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await UnitTypeMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
