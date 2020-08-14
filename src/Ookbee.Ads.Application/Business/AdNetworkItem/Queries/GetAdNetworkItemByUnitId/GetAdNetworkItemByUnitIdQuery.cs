using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetworkItem.Queries.GetAdNetworkItemByUnitId
{
    public class GetAdNetworkItemByUnitIdQuery : IRequest<HttpResult<string>>
    {
        public long AdUnitId { get; set; }
        public string Platform { get; set; }

        public GetAdNetworkItemByUnitIdQuery(long adUnitId, string platform)
        {
            AdUnitId = adUnitId;
            Platform = platform;
        }
    }
}
