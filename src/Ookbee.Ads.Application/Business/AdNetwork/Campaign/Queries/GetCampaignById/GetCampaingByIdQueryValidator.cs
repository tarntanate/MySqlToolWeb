using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryValidator : AbstractValidator<GetCampaignByIdQuery>
    {
        public GetCampaignByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
