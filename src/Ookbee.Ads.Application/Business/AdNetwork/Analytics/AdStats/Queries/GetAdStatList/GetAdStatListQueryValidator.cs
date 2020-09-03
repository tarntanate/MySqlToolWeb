using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdStats.Queries.GetAdStatsList
{
    public class GetAdStatsListQueryValidator : AbstractValidator<GetAdStatsListQuery>
    {
        public GetAdStatsListQueryValidator()
        {
            RuleFor(p => p.AdId).GreaterThan(0);
        }
    }
}
