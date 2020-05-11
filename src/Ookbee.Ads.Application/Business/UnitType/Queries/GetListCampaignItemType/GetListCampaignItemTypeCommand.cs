using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.UnitType.Queries.GetListUnitType
{
    public class GetListUnitTypeCommand : IRequest<HttpResult<IEnumerable<UnitTypeDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListUnitTypeCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
