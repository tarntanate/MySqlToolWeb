using FluentValidation;

namespace Ookbee.Ads.Application.Business.UnitType.Queries.GetListUnitType
{
    public class GetListUnitTypeCommandValidator : AbstractValidator<GetListUnitTypeCommand>
    {
        public GetListUnitTypeCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
