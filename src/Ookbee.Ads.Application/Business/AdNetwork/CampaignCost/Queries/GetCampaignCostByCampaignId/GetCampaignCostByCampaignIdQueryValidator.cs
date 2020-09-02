using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Queries.GetCampaignCostById
{
    public class GetCampaignCostByCampaignIdQueryValidator : AbstractValidator<GetCampaignCostByCampaignIdQuery>
    {
        public GetCampaignCostByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
