using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQueryValidator : AbstractValidator<GetCampaignByNameQuery>
    {
        public GetCampaignByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
