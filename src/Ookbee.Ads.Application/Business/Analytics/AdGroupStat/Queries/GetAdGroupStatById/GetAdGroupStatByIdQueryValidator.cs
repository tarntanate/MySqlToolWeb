using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatById
{
    public class GetAdGroupStatByIdQueryValidator : AbstractValidator<GetAdGroupStatByIdQuery>
    {
        public GetAdGroupStatByIdQueryValidator()
        {
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
