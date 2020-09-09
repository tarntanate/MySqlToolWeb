using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.IsExistsAdStatsById
{
    public class IsExistsAdStatsByIdQueryValidator : AbstractValidator<IsExistsAdStatsByIdQuery>
    {
        public IsExistsAdStatsByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
