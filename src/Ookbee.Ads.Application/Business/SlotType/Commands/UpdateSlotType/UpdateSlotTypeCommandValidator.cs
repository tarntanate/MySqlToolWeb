using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.UpdateSlotType
{
    public class UpdateSlotTypeCommandValidator : AbstractValidator<UpdateSlotTypeCommand>
    {
        public UpdateSlotTypeCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"SlotType '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
