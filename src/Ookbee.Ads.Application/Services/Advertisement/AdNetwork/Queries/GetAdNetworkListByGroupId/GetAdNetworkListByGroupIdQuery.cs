using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkListByGroupId
{
    public class GetAdNetworkListByGroupIdQuery : IRequest<Response<IEnumerable<AdNetworkDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long AdGroupId { get; private set; }

        public GetAdNetworkListByGroupIdQuery(int start, int length, long adGroupId)
        {
            Start = start;
            Length = length;
            AdGroupId = adGroupId;
        }
    }
}
