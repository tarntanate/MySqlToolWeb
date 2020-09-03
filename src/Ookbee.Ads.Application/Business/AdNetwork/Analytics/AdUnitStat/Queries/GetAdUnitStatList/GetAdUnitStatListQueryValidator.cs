using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdUnitStats.Queries.GetAdUnitStatsList
{
    public class GetAdUnitStatsListQueryValidator : AbstractValidator<GetAdUnitStatsListQuery>
    {
        public GetAdUnitStatsListQueryValidator()
        {
            RuleFor(p => p.AdUnitId).GreaterThan(0);
        }
    }
}
