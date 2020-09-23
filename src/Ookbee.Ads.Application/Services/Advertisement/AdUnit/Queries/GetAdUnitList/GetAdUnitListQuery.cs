using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList
{
    public class GetAdUnitListQuery : IRequest<Response<IEnumerable<AdUnitDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdGroupId { get; private set; }

        public GetAdUnitListQuery(int start, int length, long? adGroupId)
        {
            Start = start;
            Length = length;
            AdGroupId = adGroupId;
        }
    }
}
