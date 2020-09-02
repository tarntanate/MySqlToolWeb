using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Queries.IsExistsCampaignCostByCampaignId
{
    public class IsExistsCampaignCostByCampaignIdQueryValidator : AbstractValidator<IsExistsCampaignCostByCampaignIdQuery>
    {
        public IsExistsCampaignCostByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
