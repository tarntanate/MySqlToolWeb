using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeList
{
    public class GetAdUnitTypeListQuery : IRequest<HttpResult<IEnumerable<AdUnitTypeDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public GetAdUnitTypeListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
