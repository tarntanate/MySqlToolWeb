using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Queries.GetAdGroupItemListByKey
{
    public class GetGroupItemListByKeyQuery : IRequest<HttpResult<string>>
    {
        public long AdGroupId { get; set; }

        public GetGroupItemListByKeyQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
