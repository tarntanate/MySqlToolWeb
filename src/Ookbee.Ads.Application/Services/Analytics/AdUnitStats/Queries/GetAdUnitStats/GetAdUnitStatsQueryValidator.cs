using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStats
{
    public class GetAdUnitStatsQueryValidator : AbstractValidator<GetAdUnitStatsQuery>
    {
        public GetAdUnitStatsQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
