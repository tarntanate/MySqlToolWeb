using FluentValidation;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByAdUnitId
{
    public class GetAdByAdUnitIdQueryValidator : AbstractValidator<GetAdByAdUnitIdQuery>
    {
        public GetAdByAdUnitIdQueryValidator()
        {
            RuleFor(p => p.AdUnitId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
