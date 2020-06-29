using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Queries.GetCampaignImpressionById
{
    public class GetCampaignImpressionByCampaignIdQueryValidator : AbstractValidator<GetCampaignImpressionByCampaignIdQuery>
    {
        public GetCampaignImpressionByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId).GreaterThan(0);
        }
    }
}
