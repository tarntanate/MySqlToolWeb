using FluentValidation;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.CreateSlotType
{
    public class CreateSlotTypeCommandValidator : AbstractValidator<CreateSlotTypeCommand>
    {
        public CreateSlotTypeCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImageUrl).MaximumLength(250);
        }
    }
}
