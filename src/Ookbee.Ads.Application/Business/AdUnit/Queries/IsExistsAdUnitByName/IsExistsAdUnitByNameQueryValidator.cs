using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitByName
{
    public class IsExistsAdUnitByNameQueryValidator : AbstractValidator<IsExistsAdUnitByNameQuery>
    {
        public IsExistsAdUnitByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
