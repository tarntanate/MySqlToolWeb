using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetListBanner
{
    public class GetListBannerCommand : IRequest<HttpResult<IEnumerable<BannerDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListBannerCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
