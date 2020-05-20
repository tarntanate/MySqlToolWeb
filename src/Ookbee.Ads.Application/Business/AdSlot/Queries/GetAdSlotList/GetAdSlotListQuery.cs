using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotList
{
    public class GetAdSlotListQuery : IRequest<HttpResult<IEnumerable<AdSlotDto>>>
    {
        public string PublisherId { get; set; }

        public int Start { get; set; }
        
        public int Length { get; set; }

        public GetAdSlotListQuery(string publisherId, int start, int length)
        {
            PublisherId = publisherId;
            Start = start;
            Length = length;
        }
    }
}
