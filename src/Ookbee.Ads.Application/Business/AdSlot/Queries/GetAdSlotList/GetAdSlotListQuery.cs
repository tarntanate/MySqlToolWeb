using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotList
{
    public class GetAdSlotListQuery : IRequest<HttpResult<IEnumerable<AdSlotDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetAdSlotListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
