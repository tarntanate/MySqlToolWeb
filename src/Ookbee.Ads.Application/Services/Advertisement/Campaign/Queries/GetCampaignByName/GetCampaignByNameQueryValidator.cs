using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQueryValidator : AbstractValidator<GetCampaignByNameQuery>
    {
        public GetCampaignByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
