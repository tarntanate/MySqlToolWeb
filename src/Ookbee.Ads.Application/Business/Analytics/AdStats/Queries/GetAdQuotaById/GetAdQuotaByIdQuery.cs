using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdQuotaById
{
    public class GetAdQuotaByIdQuery : IRequest<Response<int>>
    {
        public long AdId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public GetAdQuotaByIdQuery(long adId, DateTimeOffset caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
