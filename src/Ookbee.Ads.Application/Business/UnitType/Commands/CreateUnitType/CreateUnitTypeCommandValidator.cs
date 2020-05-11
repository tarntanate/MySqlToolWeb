using FluentValidation;

namespace Ookbee.Ads.Application.Business.UnitType.Commands.CreateUnitType
{
    public class CreateUnitTypeCommandValidator : AbstractValidator<CreateUnitTypeCommand>
    {
        public CreateUnitTypeCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImageUrl).MaximumLength(250);
        }
    }
}
