using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStats
{
    public class GetAdStatsQueryValidator : AbstractValidator<GetAdStatsQuery>
    {
        public GetAdStatsQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
