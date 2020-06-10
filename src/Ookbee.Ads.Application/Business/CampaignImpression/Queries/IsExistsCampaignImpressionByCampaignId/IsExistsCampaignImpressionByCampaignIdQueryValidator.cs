using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Queries.IsExistsCampaignImpressionByCampaignId
{
    public class IsExistsCampaignImpressionByCampaignIdQueryValidator : AbstractValidator<IsExistsCampaignImpressionByCampaignIdQuery>
    {
        public IsExistsCampaignImpressionByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
