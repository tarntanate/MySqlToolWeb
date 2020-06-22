using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById
{
    public class IsExistsAdUnitByIdQueryValidator : AbstractValidator<IsExistsAdUnitByIdQuery>
    {
        public IsExistsAdUnitByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
