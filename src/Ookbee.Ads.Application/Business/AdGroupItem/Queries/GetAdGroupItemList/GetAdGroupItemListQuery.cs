using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemList
{
    public class GetAdGroupItemListQuery : IRequest<HttpResult<IEnumerable<AdGroupItemDto>>>
    {
        public long? AdGroupId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public GetAdGroupItemListQuery(int start, int length, long? adGroupId)
        {
            Start = start;
            Length = length;
            AdGroupId = adGroupId;
        }
    }
}
