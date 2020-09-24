using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.IsExistsAStatsByKey
{
    public class IsExistsAdStatsByKeyQueryValidator : AbstractValidator<IsExistsAdStatsByKeyQuery>
    {
        public IsExistsAdStatsByKeyQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
