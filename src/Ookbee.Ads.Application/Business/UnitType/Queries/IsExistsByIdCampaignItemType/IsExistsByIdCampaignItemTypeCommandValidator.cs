using FluentValidation;

namespace Ookbee.Ads.Application.Business.UnitType.Queries.IsExistsByIdUnitType
{
    public class IsExistsByIdUnitTypeCommandValidator : AbstractValidator<IsExistsByIdUnitTypeCommand>
    {
        public IsExistsByIdUnitTypeCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
