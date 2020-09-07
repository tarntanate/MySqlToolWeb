using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatsByKey
{
    public class IsExistsAdGroupStatsByKeyQueryValidator : AbstractValidator<IsExistsAdGroupStatsByKeyQuery>
    {
        public IsExistsAdGroupStatsByKeyQueryValidator()
        {
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
