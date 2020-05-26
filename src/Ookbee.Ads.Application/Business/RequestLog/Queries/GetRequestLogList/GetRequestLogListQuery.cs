using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Queries.GetRequestLogList
{
    public class GetRequestLogListQuery : IRequest<HttpResult<IEnumerable<RequestLogDto>>>
    {
        public string CampaignId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public GetRequestLogListQuery(string campaignId, int start, int length)
        {
            CampaignId = campaignId;
            Start = start;
            Length = length;
        }
    }
}
