using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStatsList.Queries.GetAdGroupStatsList
{
    public class GetAdGroupStatsListQueryValidator : AbstractValidator<GetAdGroupStatsListQuery>
    {
        public GetAdGroupStatsListQueryValidator()
        {
            RuleFor(p => p.AdGroupId).GreaterThan(0);
        }
    }
}
