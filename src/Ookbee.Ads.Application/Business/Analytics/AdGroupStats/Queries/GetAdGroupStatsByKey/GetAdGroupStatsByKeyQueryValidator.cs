using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey
{
    public class GetAdGroupStatsByKeyQueryValidator : AbstractValidator<GetAdGroupStatsByKeyQuery>
    {
        public GetAdGroupStatsByKeyQueryValidator()
        {
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
