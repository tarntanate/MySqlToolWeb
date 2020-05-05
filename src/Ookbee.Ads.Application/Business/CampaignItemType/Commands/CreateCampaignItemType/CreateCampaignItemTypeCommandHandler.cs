using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Commands.CreateCampaignItemType
{
    public class CreateCampaignItemTypeCommandHandler : IRequestHandler<CreateCampaignItemTypeCommand, HttpResult<string>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> CampaignItemTypeMongoDB { get; }

        public CreateCampaignItemTypeCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> campaignItemTypeMongoDB)
        {
            CampaignItemTypeMongoDB = campaignItemTypeMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateCampaignItemTypeCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignItemTypeDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CampaignItemTypeDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await CampaignItemTypeMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

    }
}
