using System;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class CreateDailySummaryCommand : IRequest<HttpResult<string>>
    {

        public string Id => ObjectId.GenerateNewId().ToString();

        public string CampaignId { get; private set; }

        public int Requests => 0;

        public int Fills => 0;

        public int Impressions => 0;

        public int Clicks => 0;

        public DateTime CaculateDate => MechineDateTime.Now.DateTime;

        public DateTime? CreatedAt => MechineDateTime.Now.DateTime;

        public CreateDailySummaryCommand(string campaignId)
        {
            CampaignId = campaignId;
        }
    }
}
