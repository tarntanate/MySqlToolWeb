using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaUrl
{
    public class UpdateMediaUrlCommandValidator : AbstractValidator<UpdateMediaUrlCommand>
    {
        public UpdateMediaUrlCommandValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"MediaFile '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.MediaUrl).MaximumLength(250);
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }

        private bool BeAValidUri(string fileUrl)
        {
            return fileUrl.IsValidUri();
        }
    }
}
