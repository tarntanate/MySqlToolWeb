using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.DeleteCampaignImpression
{
    public class DeleteCampaignImpressionCommandValidator : AbstractValidator<DeleteCampaignImpressionCommand>
    {
        public DeleteCampaignImpressionCommandValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
