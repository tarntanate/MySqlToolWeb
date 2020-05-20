using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.UpdateAdSlot
{
    public class UpdateAdCommandValidator : AbstractValidator<UpdateAdSlotCommand>
    {
        public UpdateAdCommandValidator()
        {
            RuleFor(p => p.PublisherId).Must(BeAValidObjectId).WithMessage(p => $"Publisher '{p.PublisherId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
