using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQuery : IRequest<HttpResult<string>>
    {
        public long AdUnitId { get; set; }
        public string Platform { get; set; }

        public GetAdUnitByIdQuery(long adUnitId, string platform)
        {
            AdUnitId = adUnitId;
            Platform = platform;
        }
    }
}
