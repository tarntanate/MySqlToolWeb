using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetworkUnit.Queries.GetAdNetworkUnitByUnitId
{
    public class GetAdNetworkUnitByUnitIdQuery : IRequest<HttpResult<string>>
    {
        public long AdUnitId { get; set; }
        public string Platform { get; set; }

        public GetAdNetworkUnitByUnitIdQuery(long adUnitId, string platform)
        {
            AdUnitId = adUnitId;
            Platform = platform;
        }
    }
}
