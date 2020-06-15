using FluentValidation;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Enums;
using System;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQueryValidator : AbstractValidator<GetCampaignListQuery>
    {
        public GetCampaignListQueryValidator()
        {
            RuleFor(p => p.AdvertiserId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue).When(m => m.AdvertiserId != null);
            RuleFor(p => p.PricingModel).Must(BeValidPricingModel).When(m => m.PricingModel.HasValue()).WithMessage("Only 'CPM' and 'IMP' Pricing Model is supported.");
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }

        private bool BeValidPricingModel(string value)
        {
            if (Enum.TryParse<PricingModel>(value, true, out var pricingModel))
                return true;
            return false;
        }
    }
}
