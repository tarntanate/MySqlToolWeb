using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.DeleteCampaignCost
{
    public class DeleteCampaignCostCommandValidator : AbstractValidator<DeleteCampaignCostCommand>
    {
        public DeleteCampaignCostCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");
        }
    }
}
