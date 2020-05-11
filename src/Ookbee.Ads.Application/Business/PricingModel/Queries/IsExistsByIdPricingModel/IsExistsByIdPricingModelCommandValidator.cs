using FluentValidation;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsByIdPricingModel
{
    public class IsExistsByIdPricingModelCommandValidator : AbstractValidator<IsExistsByIdPricingModelCommand>
    {
        public IsExistsByIdPricingModelCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
