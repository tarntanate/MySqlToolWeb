using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.GetAdvertiserList
{
    public class GetAdvertiserListQuery : IRequest<Response<IEnumerable<AdvertiserDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public GetAdvertiserListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
