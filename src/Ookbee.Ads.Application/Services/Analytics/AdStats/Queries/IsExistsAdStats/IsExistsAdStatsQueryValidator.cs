using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.IsExistsAdStats
{
    public class IsExistsAdStatsQueryValidator : AbstractValidator<IsExistsAdStatsQuery>
    {
        public IsExistsAdStatsQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
