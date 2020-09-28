using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatsByKey
{
    public class IsExistsAdGroupStatsByKeyQueryValidator : AbstractValidator<IsExistsAdGroupStatsByKeyQuery>
    {
        public IsExistsAdGroupStatsByKeyQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
