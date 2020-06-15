using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList
{
    public class GetAdUnitListQuery : IRequest<HttpResult<IEnumerable<AdUnitDto>>>
    {
        public long? AdUnitTypeId { get; set; }
        public long? PublisherId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public GetAdUnitListQuery(int start, int length, long? adUnitTypeId, long? publisherId)
        {
            Start = start;
            Length = length;
            AdUnitTypeId = adUnitTypeId;
            PublisherId = publisherId;
        }
    }
}
