using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupIdList
{
    public class GetAdGroupIdListQuery : IRequest<Response<IEnumerable<long>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdGroupTypeId { get; private set; }
        public long? PublisherId { get; private set; }
        public bool? Enabled { get; private set; }

        public GetAdGroupIdListQuery(int start, int length, long? adGroupTypeId, long? publisherId, bool? enabled = false)
        {
            Start = start;
            Length = length;
            AdGroupTypeId = adGroupTypeId;
            PublisherId = publisherId;
            Enabled = enabled;
        }
    }
}
