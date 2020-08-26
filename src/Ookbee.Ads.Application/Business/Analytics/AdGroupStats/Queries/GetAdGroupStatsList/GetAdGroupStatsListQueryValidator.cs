using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStatsList.Queries.GetAdGroupStatsList
{
    public class GetAdGroupStatsListQueryValidator : AbstractValidator<GetAdGroupStatsListQuery>
    {
        public GetAdGroupStatsListQueryValidator()
        {
            RuleFor(p => p.AdGroupId).GreaterThan(0);
        }
    }
}
