using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeList
{
    public class GetAdGroupTypeListQuery : IRequest<Response<IEnumerable<AdGroupTypeDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }

        public GetAdGroupTypeListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
