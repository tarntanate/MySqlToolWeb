using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommandValidator : AbstractValidator<DeleteCampaignCommand>
    {
        public DeleteCampaignCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
