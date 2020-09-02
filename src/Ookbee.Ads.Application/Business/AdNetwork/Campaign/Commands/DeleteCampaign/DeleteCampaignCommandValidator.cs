using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommandValidator : AbstractValidator<DeleteCampaignCommand>
    {
        public DeleteCampaignCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
