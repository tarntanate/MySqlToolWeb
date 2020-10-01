using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.IsExistsAdGroupStats
{
    public class IsExistsAdGroupStatsQueryValidator : AbstractValidator<IsExistsAdGroupStatsQuery>
    {
        public IsExistsAdGroupStatsQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
