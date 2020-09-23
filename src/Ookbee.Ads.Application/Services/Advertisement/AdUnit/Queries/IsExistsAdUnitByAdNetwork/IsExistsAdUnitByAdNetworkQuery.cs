using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdNetwork
{
    public class IsExistsAdUnitByAdNetworkQuery : IRequest<Response<bool>>
    {
        public string AdNetwork { get; set; }

        public IsExistsAdUnitByAdNetworkQuery(string adNetwork)
        {
            AdNetwork = adNetwork;
        }
    }
}
