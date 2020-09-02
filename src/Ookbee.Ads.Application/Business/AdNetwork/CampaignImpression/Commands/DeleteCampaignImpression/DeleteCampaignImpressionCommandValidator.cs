using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Commands.DeleteCampaignImpression
{
    public class DeleteCampaignImpressionCommandValidator : AbstractValidator<DeleteCampaignImpressionCommand>
    {
        public DeleteCampaignImpressionCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
