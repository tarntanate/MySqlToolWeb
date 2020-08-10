using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByKeyQuery;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByUnitId
{
    public class GetAdByUnitIdQuery : IRequest<HttpResult<AdDto>>
    {
        public long AdUnitId { get; set; }
        public string Platform { get; set; }

        public GetAdByUnitIdQuery(long adUnitId, string platform)
        {
            AdUnitId = adUnitId;
            Platform = platform;
        }
    }
}
