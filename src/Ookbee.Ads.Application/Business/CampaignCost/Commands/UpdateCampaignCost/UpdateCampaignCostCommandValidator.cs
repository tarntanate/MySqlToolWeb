using FluentValidation;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.UpdateCampaignCost
{
    public class UpdateCampaignCostCommandValidator : AbstractValidator<UpdateCampaignCostCommand>
    {
        public UpdateCampaignCostCommandValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.AdvertiserId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.PricingModel).IsEnumName(typeof(PricingModel), caseSensitive: false);
            RuleFor(p => p.Budget).GreaterThan(0);
            RuleFor(p => p.CostPerUnit).GreaterThan(0);
        }
    }
}
