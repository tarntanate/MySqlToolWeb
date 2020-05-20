using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.DeleteAdSlot
{
    public class DeleteAdSlotCommandValidator : AbstractValidator<DeleteAdSlotCommand>
    {
        public DeleteAdSlotCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"AdSlot '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
