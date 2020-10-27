using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAvailableQuota
{
    public class GetAvailableQuotaQuery : IRequest<Response<int>>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdId { get; set; }

        public GetAvailableQuotaQuery(DateTimeOffset caculatedAt, long adId)
        {
            CaculatedAt = caculatedAt;
            AdId = adId;
        }
    }
}
