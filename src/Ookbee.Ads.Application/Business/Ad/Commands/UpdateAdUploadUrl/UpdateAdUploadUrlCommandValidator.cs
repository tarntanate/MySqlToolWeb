using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAdUploadUrl
{
    public class UpdateAdUploadUrlCommandValidator : AbstractValidator<UpdateAdUploadUrlCommand>
    {
        public UpdateAdUploadUrlCommandValidator()
        {
            RuleFor(p => p.AdId).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.AdId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.MediaFileId).Must(BeAValidObjectId).WithMessage(p => $"MediaFile '{p.MediaFileId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.UploadFileId).Must(BeAValidObjectId).WithMessage(p => $"UploadFile '{p.UploadFileId}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
