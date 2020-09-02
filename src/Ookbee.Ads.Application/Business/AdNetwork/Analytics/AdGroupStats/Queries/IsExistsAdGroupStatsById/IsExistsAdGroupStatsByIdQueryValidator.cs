using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStatsList.Queries.IsExistsAdGroupStatsById
{
    public class IsExistsAdGroupStatsByIdQueryValidator : AbstractValidator<IsExistsAdGroupStatsByIdQuery>
    {
        public IsExistsAdGroupStatsByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
