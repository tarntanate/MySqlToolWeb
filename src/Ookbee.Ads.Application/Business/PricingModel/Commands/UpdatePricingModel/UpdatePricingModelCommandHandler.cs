using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsByIdPricingModel;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.UpdatePricingModel
{
    public class UpdatePricingModelCommandHandler : IRequestHandler<UpdatePricingModelCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public UpdatePricingModelCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<PricingModelDocument> pricingModelMongoDB)
        {
            Mediatr = mediatr;
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdatePricingModelCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<PricingModelDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(PricingModelDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdPricingModelCommand(document.Id));
                if (!isExistsByNameResult.Data)
                    return result.Fail(404, $"PricingModel '{document.Id}' doesn't exist.");

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await PricingModelMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
