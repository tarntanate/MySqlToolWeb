using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignCost.Queries.GetCampaignCostById
{
    public class GetCampaignCostByCampaignIdQueryValidator : AbstractValidator<GetCampaignCostByCampaignIdQuery>
    {
        public GetCampaignCostByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
