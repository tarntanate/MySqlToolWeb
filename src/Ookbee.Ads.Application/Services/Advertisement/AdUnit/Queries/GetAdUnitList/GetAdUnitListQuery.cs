using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList
{
    public class GetAdUnitListQuery : IRequest<Response<IEnumerable<AdUnitDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdGroupId { get; set; }

        public GetAdUnitListQuery(int start, int length, long? adGroupId)
        {
            Start = start;
            Length = length;
            AdGroupId = adGroupId;
        }
    }
}
