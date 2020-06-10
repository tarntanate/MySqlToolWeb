using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignCost.Queries.IsExistsCampaignCostById
{
    public class IsExistsCampaignCostByIdQueryValidator : AbstractValidator<IsExistsCampaignCostByIdQuery>
    {
        public IsExistsCampaignCostByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
