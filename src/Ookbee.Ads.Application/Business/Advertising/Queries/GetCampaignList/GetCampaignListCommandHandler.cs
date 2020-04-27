using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Advertising.ViewModels;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Advertising.Documents;
using Ookbee.Ads.Domain.Advertising.Entity;
using Ookbee.Ads.Persistence.Advertising.EntityFrameworkCore;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertising.Queries.GetCampaignList
{
    public class GetCampaignListCommandHandler : IRequestHandler<GetCampaignListCommand, HttpResult<IEnumerable<CampaignViewModel>>>
    {
        private OokbeeAdsEfRepository<Campaign> CampaignEfRepo { get; }
        private OokbeeAdsMongoRepository<CampaignDocument> CampaignMongoRepo { get; }

        public GetCampaignListCommandHandler(
            OokbeeAdsEfRepository<Campaign> campaignEfRepo,
            OokbeeAdsMongoRepository<CampaignDocument> campaignMongoRepo)
        {
            CampaignEfRepo = campaignEfRepo;
            CampaignMongoRepo = campaignMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<CampaignViewModel>>> Handle(GetCampaignListCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<IEnumerable<CampaignViewModel>>();
            var data = await GetCampaignOnEf(request.Start, request.Length);
            return result.Success(data);
        }

        private async Task<IEnumerable<CampaignViewModel>> GetCampaignOnEf(int start = 0, int length = 10)
        {
            var data = await CampaignEfRepo.FindAsync(
                selector: f => new CampaignViewModel()
                {
                    Id = f.Id
                },
                filter: f => f.Id > 3,
                orderBy: f => f.OrderBy(chatRoom => chatRoom.Id),
                start: start,
                length: length);
            return data;
        }

        private async Task<IEnumerable<CampaignViewModel>> GetCampaignOnMongo(int start = 0, int length = 10)
        {
            var data = await CampaignMongoRepo.FindAsync(
                selector: f => new CampaignViewModel()
                {
                    Id = f.Id
                },
                filter: f => f.Id > 3,
                sort: Builders<CampaignDocument>.Sort.Descending(nameof(CampaignDocument.Id)),
                start: start,
                length: length);
            return data;
        }
    }
}