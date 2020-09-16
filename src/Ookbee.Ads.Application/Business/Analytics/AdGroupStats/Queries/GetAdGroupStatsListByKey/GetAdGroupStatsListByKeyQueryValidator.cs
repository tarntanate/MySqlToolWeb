using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsListByKey
{
    public class GetAdGroupStatsListByKeyQueryValidator : AbstractValidator<GetAdGroupStatsListByKeyQuery>
    {
        public GetAdGroupStatsListByKeyQueryValidator()
        {
            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
