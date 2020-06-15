using System;
using FluentValidation;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQueryValidator : AbstractValidator<GetCampaignListQuery>
    {
        public GetCampaignListQueryValidator()
        {
            RuleFor(p => p.AdvertiserId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue).When(customer => customer.AdvertiserId != null);
            RuleFor(p => p.PricingModel).Must(BeValidPricingModel);
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
        
        private bool BeValidPricingModel(string value)
        {
            if (value.HasValue() && Enum.TryParse<PricingModel>(value, true, out var pricingModel))
                return true;
            return true;
        }
    }
}
