using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignCost.Queries.IsExistsCampaignCostByCampaignId
{
    public class IsExistsCampaignCostByCampaignIdQueryValidator : AbstractValidator<IsExistsCampaignCostByCampaignIdQuery>
    {
        public IsExistsCampaignCostByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");
        }
    }
}
