using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitByAdNetwork
{
    public class GetAdUnitByAdNetworkQuery : IRequest<Response<AdUnitDto>>
    {
        public string AdNetwork { get; private set; }

        public GetAdUnitByAdNetworkQuery(string adNetwork)
        {
            AdNetwork = adNetwork;
        }
    }
}
