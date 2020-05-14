using FluentValidation;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsByIdSlotType
{
    public class IsExistsByIdSlotTypeCommandValidator : AbstractValidator<IsExistsByIdSlotTypeCommand>
    {
        public IsExistsByIdSlotTypeCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
