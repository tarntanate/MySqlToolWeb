using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.IsExistsCampaignByName
{
    public class IsExistsCampaignByNameQueryValidator : AbstractValidator<IsExistsCampaignByNameQuery>
    {
        public IsExistsCampaignByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
