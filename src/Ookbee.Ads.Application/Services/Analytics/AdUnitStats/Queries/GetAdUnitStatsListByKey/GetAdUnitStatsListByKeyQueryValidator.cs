using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsListByKey
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
