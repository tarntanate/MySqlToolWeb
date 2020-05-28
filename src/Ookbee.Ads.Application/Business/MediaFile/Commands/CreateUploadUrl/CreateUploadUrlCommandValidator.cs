using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.CreateUploadUrl
{
    public class CreateUploadUrlCommandValidator : AbstractValidator<CreateUploadUrlCommand>
    {
        public CreateUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Id '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.FileExtension).NotNull().NotEmpty().Must(BeAValidFileExtension).WithMessage("Only .jpg .jpeg and .png file is supported.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }

        private bool BeAValidFileExtension(string value)
        {
            return value.IsValidJpeg() || value.IsValidPng();
        }
    }
}
