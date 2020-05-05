using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Queries.GetListCampaignItemType
{
    public class GetListCampaignItemTypeCommandHandler : IRequestHandler<GetListCampaignItemTypeCommand, HttpResult<IEnumerable<CampaignItemTypeDto>>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> CampaignItemTypeMongoDB { get; }

        public GetListCampaignItemTypeCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> CampaignItemTypeMongoRepo)
        {
            CampaignItemTypeMongoDB = CampaignItemTypeMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<CampaignItemTypeDto>>> Handle(GetListCampaignItemTypeCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<CampaignItemTypeDto>>> GetListMongoDB(GetListCampaignItemTypeCommand request)
        {
            var result = new HttpResult<IEnumerable<CampaignItemTypeDto>>();
            var items = await CampaignItemTypeMongoDB.FindAsync(
                sort: Builders<CampaignItemTypeDocument>.Sort.Descending(nameof(CampaignItemTypeDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<CampaignItemTypeDto>>();
            return result.Success(data);
        }
    }
}
