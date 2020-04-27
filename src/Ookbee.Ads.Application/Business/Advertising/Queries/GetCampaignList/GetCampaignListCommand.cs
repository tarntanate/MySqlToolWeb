using MediatR;
using Ookbee.Ads.Application.Business.Advertising.ViewModels;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertising.Queries.GetCampaignList
{
    public class GetCampaignListCommand : IRequest<HttpResult<IEnumerable<CampaignViewModel>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
    }
}
