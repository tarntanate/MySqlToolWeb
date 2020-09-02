using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Queries.IsExistsAdUnitTypeById
{
    public class IsExistsAdUnitTypeByIdQueryValidator : AbstractValidator<IsExistsAdUnitTypeByIdQuery>
    {
        public IsExistsAdUnitTypeByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
