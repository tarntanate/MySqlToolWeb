using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkList
{
    public class GetAdNetworkListQuery : IRequest<Response<IEnumerable<AdNetworkDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdUnitId { get; private set; }

        public GetAdNetworkListQuery(int start, int length, long? adUnitId)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
        }
    }
}
