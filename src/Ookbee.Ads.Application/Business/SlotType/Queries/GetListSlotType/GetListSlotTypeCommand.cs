using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetListSlotType
{
    public class GetListSlotTypeCommand : IRequest<HttpResult<IEnumerable<SlotTypeDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListSlotTypeCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
