using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.DeleteCampaignPricingModel
{
    public class DeleteCampaignPricingModelCommandValidator : AbstractValidator<DeleteCampaignPricingModelCommand>
    {
        public DeleteCampaignPricingModelCommandValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"'{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
