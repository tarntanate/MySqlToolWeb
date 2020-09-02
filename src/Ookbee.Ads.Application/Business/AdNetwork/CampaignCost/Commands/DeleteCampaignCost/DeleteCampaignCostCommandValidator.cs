using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Commands.DeleteCampaignCost
{
    public class DeleteCampaignCostCommandValidator : AbstractValidator<DeleteCampaignCostCommand>
    {
        public DeleteCampaignCostCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
