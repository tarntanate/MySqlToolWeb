using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeByName
{
    public class IsExistsAdUnitTypeByNameQueryValidator : AbstractValidator<IsExistsAdUnitTypeByNameQuery>
    {
        public IsExistsAdUnitTypeByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
