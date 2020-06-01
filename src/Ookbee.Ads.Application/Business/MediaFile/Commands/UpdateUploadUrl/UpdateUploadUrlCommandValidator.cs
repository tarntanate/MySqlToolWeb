using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateUploadUrl
{
    public class UpdateUploadUrlCommandValidator : AbstractValidator<UpdateUploadUrlCommand>
    {
        public UpdateUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"MediaFile '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.UploadUrlId).Must(BeAValidObjectId).WithMessage(p => $"UploadUrl '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
