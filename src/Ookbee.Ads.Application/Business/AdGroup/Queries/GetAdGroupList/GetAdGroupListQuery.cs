using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupList
{
    public class GetAdGroupListQuery : IRequest<HttpResult<IEnumerable<AdGroupDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdUnitTypeId { get; set; }
        public long? PublisherId { get; set; }

        public GetAdGroupListQuery(int start, int length, long? adUnitTypeId, long? publisherId)
        {
            Start = start;
            Length = length;
            AdUnitTypeId = adUnitTypeId;
            PublisherId = publisherId;
        }
    }
}
