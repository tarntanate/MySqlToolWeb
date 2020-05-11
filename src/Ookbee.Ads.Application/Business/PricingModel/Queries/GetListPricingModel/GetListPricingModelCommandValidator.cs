using FluentValidation;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetListPricingModel
{
    public class GetListPricingModelCommandValidator : AbstractValidator<GetListPricingModelCommand>
    {
        public GetListPricingModelCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
