﻿using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Commands.UpdateCampaignItemType
{
    public class UpdateCampaignItemTypeCommandValidator : AbstractValidator<UpdateCampaignItemTypeCommand>
    {
        public UpdateCampaignItemTypeCommandValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"'{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImageUrl).MaximumLength(250);
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
