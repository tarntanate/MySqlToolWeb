using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.GetAdUnitTypeList
{
    public class GetAdUnitTypeListQueryValidator : AbstractValidator<GetAdUnitTypeListQuery>
    {
        public GetAdUnitTypeListQueryValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
