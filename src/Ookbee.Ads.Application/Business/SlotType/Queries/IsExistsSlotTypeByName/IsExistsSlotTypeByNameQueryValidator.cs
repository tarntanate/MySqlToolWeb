using FluentValidation;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeByName
{
    public class IsExistsSlotTypeByNameQueryValidator : AbstractValidator<IsExistsSlotTypeByNameQuery>
    {
        public IsExistsSlotTypeByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
