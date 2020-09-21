using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Queries.IsExistsCampaignByName
{
    public class IsExistsCampaignByNameQueryValidator : AbstractValidator<IsExistsCampaignByNameQuery>
    {
        public IsExistsCampaignByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
