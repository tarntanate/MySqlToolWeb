using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdNetworkNameByAdGroup
{
    public class IsExistsAdNetworkNameByAdGroupQuery : IRequest<HttpResult<bool>>
    {
        public string AdNetworkName { get; set; }
        public long AdGroupId { get; set; }

        public IsExistsAdNetworkNameByAdGroupQuery(string adNetworkName, long adGroupId)
        {
            AdNetworkName = adNetworkName;
            AdGroupId = adGroupId;
        }
    }
}
