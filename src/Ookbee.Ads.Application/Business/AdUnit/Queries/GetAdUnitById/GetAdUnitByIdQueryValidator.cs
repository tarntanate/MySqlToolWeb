using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQueryValidator : AbstractValidator<GetAdUnitByIdQuery>
    {
        public GetAdUnitByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
