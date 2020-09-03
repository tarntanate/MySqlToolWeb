using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Queries.GetAdGroupStatsList
{
    public class GetAdGroupStatsListQueryValidator : AbstractValidator<GetAdGroupStatsListQuery>
    {
        public GetAdGroupStatsListQueryValidator()
        {
            RuleFor(p => p.AdGroupId).GreaterThan(0);
        }
    }
}
