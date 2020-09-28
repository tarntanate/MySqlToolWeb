using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStats.Queries.IsExistsAdUnitStatsById
{
    public class IsExistsAdUnitStatsByIdQueryValidator : AbstractValidator<IsExistsAdUnitStatsByIdQuery>
    {
        public IsExistsAdUnitStatsByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
