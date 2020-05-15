using System;
using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdCommandValidator : AbstractValidator<UpdateAdCommand>
    {
        public UpdateAdCommandValidator()
        {
            RuleFor(p => p.CampaignId).Must(BeAValidObjectId).WithMessage(p => $"Campaign '{p.CampaignId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.AdSlotId).Must(BeAValidObjectId).WithMessage(p => $"AdSlot '{p.AdSlotId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.Cooldown).GreaterThan(TimeSpan.FromSeconds(0)).LessThanOrEqualTo(TimeSpan.FromSeconds(30));
            RuleFor(p => p.BackgroundColor).Must(BeARGBHexColor).WithMessage("BackgroundColor only support rgb format.");
            RuleFor(p => p.ForegroundColor).Must(BeARGBHexColor).WithMessage("ForegroundColor only support rgb format.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }

        private bool BeARGBHexColor(string value)
        {
            return value.IsValidRGBHexColor();
        }
    }
}
