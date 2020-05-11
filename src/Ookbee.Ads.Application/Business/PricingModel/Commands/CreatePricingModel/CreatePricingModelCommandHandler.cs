using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.CreatePricingModel
{
    public class CreatePricingModelCommandHandler : IRequestHandler<CreatePricingModelCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public CreatePricingModelCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<PricingModelDocument> pricingModelMongoDB)
        {
            Mediatr = mediatr;
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreatePricingModelCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<PricingModelDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(PricingModelDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await PricingModelMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
