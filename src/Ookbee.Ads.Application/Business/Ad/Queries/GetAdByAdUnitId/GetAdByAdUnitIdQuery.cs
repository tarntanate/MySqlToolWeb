using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByAdUnitId
{
    public class GetAdByAdUnitIdQuery : IRequest<HttpResult<BannerDto>>
    {
        public long AdUnitId { get; set; }

        public GetAdByAdUnitIdQuery(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
