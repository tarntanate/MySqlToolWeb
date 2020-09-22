using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.GetAdUnitTypeList
{
    public class GetAdUnitTypeListQuery : IRequest<Response<IEnumerable<AdUnitTypeDto>>>
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
