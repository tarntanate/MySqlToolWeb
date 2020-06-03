using FluentValidation;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.CreatePricingModel
{
    public class CreatePricingModelCommandValidator : AbstractValidator<CreatePricingModelCommand>
    {
        public CreatePricingModelCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
