﻿using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.CreateAdSlot
{
    public class CreateAdSlotCommandValidator : AbstractValidator<CreateAdSlotCommand>
    {
        public CreateAdSlotCommandValidator()
        {
            RuleFor(p => p.PublisherId).Must(BeAValidObjectId).WithMessage(p => $"App '{p.PublisherId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
