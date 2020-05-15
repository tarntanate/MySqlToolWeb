using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeList
{
    public class GetSlotTypeListQuery : IRequest<HttpResult<IEnumerable<SlotTypeDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetSlotTypeListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
