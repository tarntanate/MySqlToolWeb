using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsById
{
    public class GetAdGroupStatsByIdQueryValidator : AbstractValidator<GetAdGroupStatsByIdQuery>
    {
        public GetAdGroupStatsByIdQueryValidator()
        {
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
