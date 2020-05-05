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

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetListCampaign
{
    public class GetListCampaignCommandHandler : IRequestHandler<GetListCampaignCommand, HttpResult<IEnumerable<CampaignDto>>>
    {
        private OokbeeAdsMongoDBRepository<CampaignDocument> CampaignMongoDB { get; }

        public GetListCampaignCommandHandler(OokbeeAdsMongoDBRepository<CampaignDocument> CampaignMongoRepo)
        {
            CampaignMongoDB = CampaignMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<CampaignDto>>> Handle(GetListCampaignCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<CampaignDto>>> GetListMongoDB(GetListCampaignCommand request)
        {
            var result = new HttpResult<IEnumerable<CampaignDto>>();
            var items = await CampaignMongoDB.FindAsync(
                sort: Builders<CampaignDocument>.Sort.Descending(nameof(CampaignDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<CampaignDto>>();
            return result.Success(data);
        }
    }
}
