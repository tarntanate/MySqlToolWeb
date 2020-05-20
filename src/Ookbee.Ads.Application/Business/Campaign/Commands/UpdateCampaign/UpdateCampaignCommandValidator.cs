using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandValidator : AbstractValidator<UpdateCampaignCommand>
    {
        public UpdateCampaignCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImageUrl).MaximumLength(250);
            RuleFor(p => p.Budget).GreaterThan(0);
            RuleFor(p => p.AdvertiserId).Length(24);
            RuleFor(p => p.AdvertiserId).Must(BeAValidObjectId).WithMessage(p => $"Advertiser '{p.AdvertiserId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.PricingModelId).Length(24);
            RuleFor(p => p.PricingModelId).Must(BeAValidObjectId).WithMessage(p => $"PricingModel '{p.PricingModelId}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
