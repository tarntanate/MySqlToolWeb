using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.IsExistsAStatsByKey
{
    public class IsExistsAdStatsByKeyQueryValidator : AbstractValidator<IsExistsAdStatsByKeyQuery>
    {
        public IsExistsAdStatsByKeyQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
