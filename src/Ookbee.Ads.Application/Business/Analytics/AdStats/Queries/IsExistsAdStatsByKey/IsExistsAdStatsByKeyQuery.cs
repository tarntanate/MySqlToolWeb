using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.IsExistsAStatsByKey
{
    public class IsExistsAdStatsByKeyQuery : IRequest<Response<bool>>
    {
        public long AdId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public IsExistsAdStatsByKeyQuery(long adId, DateTimeOffset caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
