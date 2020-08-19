using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Queries.GetAdUnitListByGroup
{
    public class GetAdUnitListByGroupQuery : IRequest<HttpResult<string>>
    {
        public long AdGroupId { get; set; }

        public GetAdUnitListByGroupQuery(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
