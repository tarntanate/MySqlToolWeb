using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetworkGroup.Queries.GetAdAdNetworkGroupListByKey
{
    public class GetAdNetworkGroupListByKeyQuery : IRequest<HttpResult<string>>
    {
        public long AdGroupId { get; set; }

        public GetAdNetworkGroupListByKeyQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
