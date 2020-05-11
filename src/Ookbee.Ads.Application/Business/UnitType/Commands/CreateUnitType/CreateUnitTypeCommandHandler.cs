using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UnitType.Commands.CreateUnitType
{
    public class CreateUnitTypeCommandHandler : IRequestHandler<CreateUnitTypeCommand, HttpResult<string>>
    {
        private OokbeeAdsMongoDBRepository<UnitTypeDocument> UnitTypeMongoDB { get; }

        public CreateUnitTypeCommandHandler(OokbeeAdsMongoDBRepository<UnitTypeDocument> unitTypeMongoDB)
        {
            UnitTypeMongoDB = unitTypeMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<UnitTypeDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(UnitTypeDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await UnitTypeMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

    }
}
