using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotByName
{
    public class IsExistsAdSlotByNameQueryValidator : AbstractValidator<IsExistsAdSlotByNameQuery>
    {
        public IsExistsAdSlotByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
