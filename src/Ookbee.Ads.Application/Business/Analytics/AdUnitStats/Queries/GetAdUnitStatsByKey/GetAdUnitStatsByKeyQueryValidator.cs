using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey
{
    public class GetAdUnitStatsByKeyQueryValidator : AbstractValidator<GetAdUnitStatsByKeyQuery>
    {
        public GetAdUnitStatsByKeyQueryValidator()
        {
            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
