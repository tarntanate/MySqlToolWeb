using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroup
{
    public class IsExistsAdUnitByAdGroupQuery : IRequest<Response<bool>>
    {
        public string AdNetworkName { get; private set; }
        public long AdGroupId { get; private set; }

        public IsExistsAdUnitByAdGroupQuery(string adNetworkName, long adGroupId)
        {
            AdNetworkName = adNetworkName;
            AdGroupId = adGroupId;
        }
    }
}
