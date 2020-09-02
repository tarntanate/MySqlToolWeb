using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Queries.IsExistsCampaignCostById
{
    public class IsExistsCampaignCostByIdQueryValidator : AbstractValidator<IsExistsCampaignCostByIdQuery>
    {
        public IsExistsCampaignCostByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
