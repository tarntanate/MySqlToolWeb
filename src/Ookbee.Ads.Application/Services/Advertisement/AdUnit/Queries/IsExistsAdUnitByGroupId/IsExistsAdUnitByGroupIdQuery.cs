using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroupId
{
    public class IsExistsAdUnitByGroupIdQuery : IRequest<Response<bool>>
    {
        public string AdNetworkName { get; private set; }
        public long AdGroupId { get; private set; }

        public IsExistsAdUnitByGroupIdQuery(string adNetworkName, long adGroupId)
        {
            AdNetworkName = adNetworkName;
            AdGroupId = adGroupId;
        }
    }
}
