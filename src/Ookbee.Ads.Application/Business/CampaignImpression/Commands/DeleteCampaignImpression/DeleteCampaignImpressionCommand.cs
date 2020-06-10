using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.DeleteCampaignImpression
{
    public class DeleteCampaignImpressionCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteCampaignImpressionCommand(long id)
        {
            Id = id;
        }
    }
}
