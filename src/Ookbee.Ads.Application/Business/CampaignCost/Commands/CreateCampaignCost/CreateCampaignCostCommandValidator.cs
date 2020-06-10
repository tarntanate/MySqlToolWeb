using FluentValidation;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.CreateCampaignCost
{
    public class CreateCampaignCostCommandValidator : AbstractValidator<CreateCampaignCostCommand>
    {
        public CreateCampaignCostCommandValidator()
        {
            RuleFor(p => p.AdvertiserId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.StartDate).GreaterThanOrEqualTo(MechineDateTime.Now);
            RuleFor(p => p.EndDate).GreaterThanOrEqualTo(MechineDateTime.Now);
            RuleFor(p => p.PricingModel).IsEnumName(typeof(PricingModel), caseSensitive: false);
            RuleFor(p => p.Budget).GreaterThan(0);
            RuleFor(p => p.CostPerUnit).GreaterThan(0);
        }
    }
}
