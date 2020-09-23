using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeById
{
    public class IsExistsAdUnitTypeByIdQueryValidator : AbstractValidator<IsExistsAdUnitTypeByIdQuery>
    {
        public IsExistsAdUnitTypeByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}
