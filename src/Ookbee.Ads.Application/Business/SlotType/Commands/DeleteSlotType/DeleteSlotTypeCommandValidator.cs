﻿using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.DeleteSlotType
{
    public class DeleteSlotTypeCommandValidator : AbstractValidator<DeleteSlotTypeCommand>
    {
        public DeleteSlotTypeCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"SlotType '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
