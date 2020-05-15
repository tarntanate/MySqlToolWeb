using FluentValidation;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeList
{
    public class GetSlotTypeListQueryValidator : AbstractValidator<GetSlotTypeListQuery>
    {
        public GetSlotTypeListQueryValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
