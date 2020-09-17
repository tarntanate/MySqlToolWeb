using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsListByKey
{
    public class GetAdUnitStatsListByKeyQueryValidator : AbstractValidator<GetAdUnitStatsListByKeyQuery>
    {
        public GetAdUnitStatsListByKeyQueryValidator()
        {
            RuleFor(p => p.AdUnitId)
                .GreaterThan(0);
        }
    }
}
