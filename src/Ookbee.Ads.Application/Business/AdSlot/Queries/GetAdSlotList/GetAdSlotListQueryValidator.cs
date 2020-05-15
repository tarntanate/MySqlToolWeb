using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotList
{
    public class GetAdSlotListQueryValidator : AbstractValidator<GetAdSlotListQuery>
    {
        public GetAdSlotListQueryValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
