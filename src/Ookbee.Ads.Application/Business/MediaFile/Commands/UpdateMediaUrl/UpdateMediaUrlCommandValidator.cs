using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaUrl
{
    public class UpdateMediaUrlCommandValidator : AbstractValidator<UpdateMediaUrlCommand>
    {
        public UpdateMediaUrlCommandValidator()
        {
            RuleFor(p => p.CampaignId).Must(BeAValidObjectId).WithMessage(p => $"Campaign '{p.CampaignId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.AdId).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"MediaFile '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.MediaUrl).MaximumLength(250);
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }

        private bool BeAValidUri(string value)
        {
            return value.IsValidUri();
        }
    }
}
