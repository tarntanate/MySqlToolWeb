using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatById
{
    public class IsExistsAdGroupStatByIdQueryValidator : AbstractValidator<IsExistsAdGroupStatByIdQuery>
    {
        public IsExistsAdGroupStatByIdQueryValidator()
        {
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
