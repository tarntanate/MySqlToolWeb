using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeById
{
    public class GetAdUnitTypeByIdQueryValidator : AbstractValidator<GetAdUnitTypeByIdQuery>
    {
        public GetAdUnitTypeByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
