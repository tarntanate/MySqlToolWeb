using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.IsExistsCampaignByName
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
