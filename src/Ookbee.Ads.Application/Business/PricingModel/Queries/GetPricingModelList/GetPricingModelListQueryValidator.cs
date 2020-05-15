using FluentValidation;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelList
{
    public class GetPricingModelListQueryValidator : AbstractValidator<GetPricingModelListQuery>
    {
        public GetPricingModelListQueryValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
