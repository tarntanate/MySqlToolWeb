using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.GetByIdCampaignItemAsset
{
    public class GetByIdCampaignItemAssetCommandValidator : AbstractValidator<GetByIdCampaignItemAssetCommand>
    {
        public GetByIdCampaignItemAssetCommandValidator()
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
