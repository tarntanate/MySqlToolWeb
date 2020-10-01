using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsList
{
    public class GetAdUnitStatsListQueryValidator : AbstractValidator<GetAdUnitStatsListQuery>
    {
        public GetAdUnitStatsListQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0);
        }
    }
}
