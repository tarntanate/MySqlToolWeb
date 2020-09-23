using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.GetAdUnitTypeList
{
    public class GetAdUnitTypeListQuery : IRequest<Response<IEnumerable<AdUnitTypeDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }

        public GetAdUnitTypeListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
