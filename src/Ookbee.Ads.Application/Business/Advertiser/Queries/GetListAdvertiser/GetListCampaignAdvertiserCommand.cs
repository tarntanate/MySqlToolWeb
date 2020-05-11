using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetListAdvertiser
{
    public class GetListAdvertiserCommand : IRequest<HttpResult<IEnumerable<AdvertiserDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListAdvertiserCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
