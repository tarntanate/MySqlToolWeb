using FluentValidation;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetListSlotType
{
    public class GetListSlotTypeCommandValidator : AbstractValidator<GetListSlotTypeCommand>
    {
        public GetListSlotTypeCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
