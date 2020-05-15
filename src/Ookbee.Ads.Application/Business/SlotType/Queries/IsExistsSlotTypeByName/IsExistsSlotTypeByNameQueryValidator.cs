using FluentValidation;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeByName
{
    public class IsExistsSlotTypeByNameQueryValidator : AbstractValidator<IsExistsSlotTypeByNameQuery>
    {
        public IsExistsSlotTypeByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
