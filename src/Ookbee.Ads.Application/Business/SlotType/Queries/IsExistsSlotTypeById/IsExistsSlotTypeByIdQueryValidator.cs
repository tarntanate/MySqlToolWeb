using FluentValidation;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeById
{
    public class IsExistsSlotTypeByIdQueryValidator : AbstractValidator<IsExistsSlotTypeByIdQuery>
    {
        public IsExistsSlotTypeByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
