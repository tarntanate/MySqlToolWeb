using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Queries.IsExistsAdUnitStatsById
{
    public class IsExistsAdUnitStatsByIdQueryValidator : AbstractValidator<IsExistsAdUnitStatsByIdQuery>
    {
        public IsExistsAdUnitStatsByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
