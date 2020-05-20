using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotList
{
    public class GetAdSlotListQueryValidator : AbstractValidator<GetAdSlotListQuery>
    {
        public GetAdSlotListQueryValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
            RuleFor(p => p.PublisherId).Must(BeAValidPublisherId).WithMessage(p => $"Publisher '{p.PublisherId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.SlotTypeId).Must(BeAValidSlotTypeId).WithMessage(p => $"SlotType '{p.SlotTypeId}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }

        private bool BeAValidPublisherId(string value)
        {
            if (value.HasValue())
                return BeAValidObjectId(value);
            return true;
        }

        private bool BeAValidSlotTypeId(string value)
        {
            if (value.HasValue())
                return BeAValidObjectId(value);
            return true;
        }
    }
}
