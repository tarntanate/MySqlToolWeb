using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Queries.GetCampaignImpressionById
{
    public class GetCampaignImpressionByCampaignIdQueryValidator : AbstractValidator<GetCampaignImpressionByCampaignIdQuery>
    {
        public GetCampaignImpressionByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId).GreaterThan(0);
        }
    }
}
