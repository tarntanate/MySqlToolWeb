using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Queries.GetAdByUnitId
{
    public class GetAdByUnitIdQuery : IRequest<HttpResult<string>>
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
