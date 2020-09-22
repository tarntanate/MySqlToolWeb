using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.GetAdNetworkList
{
    public class GetAdNetworkListQuery : IRequest<Response<IEnumerable<AdNetworkDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdUnitId { get; set; }

        public GetAdNetworkListQuery(int start, int length, long? adUnitId)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
        }
    }
}
