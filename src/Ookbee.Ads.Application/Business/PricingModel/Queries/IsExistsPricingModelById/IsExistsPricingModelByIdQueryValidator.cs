using FluentValidation;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById
{
    public class IsExistsPricingModelByIdQueryValidator : AbstractValidator<IsExistsPricingModelByIdQuery>
    {
        public IsExistsPricingModelByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
