using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Commands.DeleteCampaignAdvertiser
{
    public class DeleteCampaignAdvertiserCommandValidator : AbstractValidator<DeleteCampaignAdvertiserCommand>
    {
        public DeleteCampaignAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"'{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
