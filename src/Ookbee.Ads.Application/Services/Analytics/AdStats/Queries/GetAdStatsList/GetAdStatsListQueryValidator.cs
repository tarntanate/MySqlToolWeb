using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdStat.Queries.GetAdStatsList
{
    public class GetAdGroupStatsListQueryValidator : AbstractValidator<GetAdStatsListQuery>
    {
        public GetAdGroupStatsListQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
