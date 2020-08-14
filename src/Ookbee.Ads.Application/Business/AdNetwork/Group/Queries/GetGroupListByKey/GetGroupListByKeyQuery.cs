using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Group.Queries.GetAdGroupListByKey
{
    public class GetGroupListByKeyQuery : IRequest<HttpResult<string>>
    {
        public long AdGroupId { get; set; }

        public GetGroupListByKeyQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
