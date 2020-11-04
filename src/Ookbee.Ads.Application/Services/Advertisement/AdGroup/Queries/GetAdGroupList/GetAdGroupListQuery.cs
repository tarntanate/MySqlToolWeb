using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupList
{
    public class GetAdGroupListQuery : IRequest<Response<IEnumerable<AdGroupDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdGroupTypeId { get; private set; }
        public long? PublisherId { get; private set; }
        public bool? Enabled { get; private set; }

        public GetAdGroupListQuery(int start, int length, long? adGroupTypeId, long? publisherId, bool? enabled)
        {
            Start = start;
            Length = length;
            AdGroupTypeId = adGroupTypeId;
            PublisherId = publisherId;
            Enabled = enabled;
        }
    }
}
