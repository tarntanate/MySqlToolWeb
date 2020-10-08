using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupIdList
{
    public class GetAdGroupIdListQuery : IRequest<Response<IEnumerable<long>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdUnitTypeId { get; private set; }
        public long? PublisherId { get; private set; }
        public bool OnlyEnabledGroup { get; private set; }

        public GetAdGroupIdListQuery(int start, int length, long? adUnitTypeId, long? publisherId, bool onlyEnabledGroup = false)
        {
            Start = start;
            Length = length;
            AdUnitTypeId = adUnitTypeId;
            PublisherId = publisherId;
            OnlyEnabledGroup = onlyEnabledGroup;
        }
    }
}
