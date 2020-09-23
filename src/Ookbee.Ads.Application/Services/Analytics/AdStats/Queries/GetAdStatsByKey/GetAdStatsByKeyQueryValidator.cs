using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStatsByKey
{
    public class GetAdStatsByKeyQueryValidator : AbstractValidator<GetAdStatsByKeyQuery>
    {
        public GetAdStatsByKeyQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
