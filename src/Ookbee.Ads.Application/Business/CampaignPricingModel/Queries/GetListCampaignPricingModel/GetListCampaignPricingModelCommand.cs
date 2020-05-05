using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetListCampaignPricingModel
{
    public class GetListCampaignPricingModelCommand : IRequest<HttpResult<IEnumerable<CampaignPricingModelDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListCampaignPricingModelCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
