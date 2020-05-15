using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerList
{
    public class GetBannerListQuery : IRequest<HttpResult<IEnumerable<BannerDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetBannerListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
