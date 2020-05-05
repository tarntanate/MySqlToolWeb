using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetByIdCampaignPricingModel
{
    public class GetByIdCampaignPricingModelCommandValidator : AbstractValidator<GetByIdCampaignPricingModelCommand>
    {
        public GetByIdCampaignPricingModelCommandValidator()
        {
            RuleFor(p => p.Id).Length(24);
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"'{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
