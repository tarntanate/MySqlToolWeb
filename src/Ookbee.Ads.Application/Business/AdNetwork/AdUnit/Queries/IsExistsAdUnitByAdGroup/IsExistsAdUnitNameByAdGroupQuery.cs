using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.IsExistsAdUnitByAdGroup
{
    public class IsExistsAdUnitByAdGroupQuery : IRequest<HttpResult<bool>>
    {
        public string AdNetworkName { get; set; }
        public long AdGroupId { get; set; }

        public IsExistsAdUnitByAdGroupQuery(string adNetworkName, long adGroupId)
        {
            AdNetworkName = adNetworkName;
            AdGroupId = adGroupId;
        }
    }
}
