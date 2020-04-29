using System.Collections.Generic;
using MediatR;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.ViewModels;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.GetListAdvertiser
{
    public class GetListAdvertiserCommand : IRequest<HttpResult<IEnumerable<AdvertiserViewModel>>>
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
